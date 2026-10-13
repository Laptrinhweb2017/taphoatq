window.bluetoothPrinter = {
    device: null,
    characteristic: null,

    async connectAndPrint(dataArray) {
        try {
            // Kiểm tra kết nối cũ, nếu mất kết nối thì yêu cầu chọn thiết bị
            if (!this.device || !this.device.gatt.connected) {
                this.device = await navigator.bluetooth.requestDevice({
                    // Cho phép quét tất cả thiết bị Bluetooth xung quanh
                    acceptAllDevices: true,
                    optionalServices: [
                        '00001101-0000-1000-8000-00805f9b34fb', // SPP Standard
                        '0000ffe0-0000-1000-8000-00805f9b34fb', // MP-210 / Generic Thermal Printer Service
                        '0000ffe1-0000-1000-8000-00805f9b34fb',
                        '49535343-fe7d-4351-8727-576526531184'
                    ]
                });

                const server = await this.device.gatt.connect();
                const services = await server.getPrimaryServices();

                if (!services || services.length === 0) {
                    throw new Error("Không tìm thấy Service trên máy in MP-210");
                }

                // Tự động quét tìm Characteristic có quyền ghi (Write / WriteWithoutResponse)
                this.characteristic = null;
                for (const service of services) {
                    const characteristics = await service.getCharacteristics();
                    const targetChar = characteristics.find(c =>
                        c.properties.write || c.properties.writeWithoutResponse
                    );
                    if (targetChar) {
                        this.characteristic = targetChar;
                        break;
                    }
                }

                if (!this.characteristic) {
                    throw new Error("Không tìm thấy kênh ghi dữ liệu (Characteristic) của MP-210");
                }
            }

            // Chia nhỏ dữ liệu gửi (Gửi 100 bytes/lần để tránh ngắt kết nối Bluetooth trên MP-210)
            const byteArray = new Uint8Array(dataArray);
            const chunkSize = 100;
            for (let i = 0; i < byteArray.length; i += chunkSize) {
                const chunk = byteArray.slice(i, i + chunkSize);
                if (this.characteristic.properties.writeWithoutResponse) {
                    await this.characteristic.writeValueWithoutResponse(chunk);
                } else {
                    await this.characteristic.writeValue(chunk);
                }
            }

            return { success: true };
        } catch (error) {
            Console.error("MP-210 Bluetooth Error:", error);
            return { success: false, error: error.message };
        }
    }
};