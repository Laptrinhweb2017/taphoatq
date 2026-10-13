window.checkInHelper = {
    // ================= PROPERTIES =================
    gpsWatchId: null,
    lastRecordedLocation: null,
    localMediaStream: null,
    domObserver: null,
    clientClockInterval: null,

    // ================= LOGGING HELPER =================
    log: function (level, message, data = "") {
        const timestamp = new Date().toISOString();
        const prefix = `[CheckInHelper][${timestamp}] [${level.toUpperCase()}]`;
        if (level === "error") console.error(`${prefix} ${message}`, data);
        else if (level === "warn") console.warn(`${prefix} ${message}`, data);
        else console.log(`${prefix} ${message}`, data);
    },

    // ================= GPS METHODS =================
    getImmediateLocation: function () {
        this.log("info", "👉 Gọi getImmediateLocation()");
        if (this.lastRecordedLocation !== null) {
            this.log("info", "📍 Sử dụng vị trí lưu tạm có sẵn:", this.lastRecordedLocation);
            return this.lastRecordedLocation;
        }

        return new Promise((resolve) => {
            this.log("info", "📡 Bắt đầu quét vị trí bằng getCurrentPosition...");
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    const currentPos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                        accuracy: position.coords.accuracy,
                        isFake: false,
                        fakeReason: ""
                    };
                    this.lastRecordedLocation = currentPos;
                    this.log("info", "✅ Lấy vị trí thành công (getImmediateLocation):", currentPos);
                    resolve(currentPos);
                },
                (error) => {
                    this.log("error", "❌ Lỗi getCurrentPosition trong getImmediateLocation:", error);
                    resolve(null);
                },
                { enableHighAccuracy: true, timeout: 4000, maximumAge: 0 }
            );
        });
    },

    startGpsWatching: function () {
        this.log("info", "👉 Gọi startGpsWatching()");
        if (this.gpsWatchId !== null) {
            this.log("warn", `⚠️ gpsWatchId (${this.gpsWatchId}) đã tồn tại. Tiến hành xóa để làm mới...`);
            navigator.geolocation.clearWatch(this.gpsWatchId);
        }

        this.gpsWatchId = navigator.geolocation.watchPosition(
            (position) => {
                this.lastRecordedLocation = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                    accuracy: position.coords.accuracy,
                    isFake: false,
                    fakeReason: ""
                };
                this.log("info", "🛰️ [WatchGPS] Vị trí cập nhật liên tục:", this.lastRecordedLocation);
            },
            (error) => {
                this.log("error", "❌ [WatchGPS] Lỗi theo dõi vị trí liên tục:", error);
            },
            { enableHighAccuracy: true, timeout: 10000, maximumAge: 0 }
        );
        this.log("info", `✅ Đã thiết lập định vị liên tục với ID: ${this.gpsWatchId}`);
    },

    stopGpsWatching: function () {
        this.log("info", "👉 Gọi stopGpsWatching()");
        if (this.gpsWatchId !== null) {
            navigator.geolocation.clearWatch(this.gpsWatchId);
            this.log("info", `⚙️ Đã clearWatch thành công ID: ${this.gpsWatchId}`);
            this.gpsWatchId = null;
            this.lastRecordedLocation = null;
        } else {
            this.log("warn", "⚠️ Không có gpsWatchId nào đang chạy để dừng.");
        }
    },

    getLocationAndVerify: function () {
        this.log("info", "👉 Gọi getLocationAndVerify()");
        return new Promise((resolve, reject) => {
            if (this.lastRecordedLocation !== null) {
                this.log("info", "📍 Trả về vị trí lưu tạm ngay lập tức:", this.lastRecordedLocation);
                return resolve(this.lastRecordedLocation);
            }

            this.log("warn", "⚠️ Chưa có vị trí lưu tạm, tiến hành quét cưỡng bức getCurrentPosition...");
            navigator.geolocation.getCurrentPosition(
                (position) => {
                    this.lastRecordedLocation = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                        accuracy: position.coords.accuracy,
                        isFake: false,
                        fakeReason: ""
                    };
                    this.log("info", "✅ Quét cưỡng bức thành công:", this.lastRecordedLocation);
                    resolve(this.lastRecordedLocation);
                },
                (error) => {
                    let msg = "Chưa thu thập được dữ liệu vị trí. ";
                    if (error.code === 1) msg += "Người dùng từ chối cấp quyền.";
                    else if (error.code === 2) msg += "Không có tín hiệu GPS (Hãy bật Vị trí thiết bị).";
                    else if (error.code === 3) msg += "Hết thời gian tìm tín hiệu GPS.";

                    this.log("error", `❌ Quét cưỡng bức thất bại: ${msg}`, error);
                    reject(new Error(msg));
                },
                { enableHighAccuracy: true, timeout: 5000, maximumAge: 0 }
            );
        });
    },

    getLocation: async function () {
        this.log("info", "👉 Bắt đầu vòng lặp getLocation() tối ưu độ chính xác (Thử tối đa 5 lần)");
        let bestPos = null;
        for (let i = 0; i < 5; i++) {
            this.log("info", `🔄 Lần thử quét vị trí thứ ${i + 1}/5...`);
            try {
                const pos = await new Promise((resolve, reject) => {
                    navigator.geolocation.getCurrentPosition(resolve, reject, {
                        enableHighAccuracy: true,
                        timeout: 5000,
                        maximumAge: 0
                    });
                });

                this.log("info", `🎯 Lần thử ${i + 1} thành công. Độ chính xác hiện tại: ${pos.coords.accuracy}m`);

                if (!bestPos || pos.coords.accuracy < bestPos.coords.accuracy) {
                    bestPos = pos;
                }

                if (pos.coords.accuracy <= 20) {
                    this.log("info", "🎯 Độ chính xác đã đạt yêu cầu (<= 20m). Dừng vòng lặp sớm.");
                    break;
                }
            } catch (err) {
                this.log("warn", `⚠️ Lỗi xảy ra tại lần thử ${i + 1}:`, err);
            }
            await new Promise(r => setTimeout(r, 800));
        }

        if (!bestPos) {
            this.log("error", "❌ Qua 5 lần thử đều không lấy được vị trí nào hợp lệ.");
            throw new Error("Không lấy được vị trí");
        }

        const result = {
            lat: bestPos.coords.latitude,
            lng: bestPos.coords.longitude,
            accuracy: bestPos.coords.accuracy
        };
        this.log("info", "✅ Vị trí tốt nhất tìm được:", result);
        return result;
    },

    // ================= CLIENT CLOCK =================
    initClientClock: function (initialServerTimeTicks, elementId) {
        this.log("info", `👉 Khởi tạo initClientClock cho Element ID: [${elementId}]`);
        const el = document.getElementById(elementId);
        if (!el) {
            this.log("error", `❌ Không tìm thấy phần tử HTML hiển thị đồng hồ với ID: ${elementId}`);
            return;
        }

        let serverTimeMs = (initialServerTimeTicks - 621355968000000000) / 10000;

        if (this.clientClockInterval) {
            this.log("warn", "⚠️ Đồng hồ cũ đang chạy, tiến hành Clear Interval cũ.");
            clearInterval(this.clientClockInterval);
        }

        const formatter = new Intl.DateTimeFormat('en-GB', {
            timeZone: 'UTC',
            day: '2-digit',
            month: '2-digit',
            year: 'numeric',
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
            hour12: false
        });

        this.clientClockInterval = setInterval(() => {
            serverTimeMs += 1000;
            const date = new Date(serverTimeMs);
            const currentEl = document.getElementById(elementId);
            if (currentEl) {
                currentEl.innerText = formatter.format(date).replace(',', '');
            } else {
                this.log("warn", `⚠️ Phần tử [${elementId}] đã biến mất khỏi DOM. Tự động hủy Interval đồng hồ.`);
                clearInterval(this.clientClockInterval);
                this.clientClockInterval = null;
            }
        }, 1000);
        this.log("info", "✅ Kích hoạt đồng hồ đếm giây thành công.");
    },

    // ================= CAMERA METHODS =================
    startCamera: function (videoElementId) {
        this.log("info", `👉 Khởi chạy startCamera() cho thẻ video: [${videoElementId}]`);

        // 📌 BƯỚC THẦN THÁNH: Nếu có luồng cũ từ đợt render lỗi trước đó, tắt nó ngay!
        if (this.localMediaStream) {
            this.log("warn", "🚨 Phát hiện luồng camera cũ chưa được giải phóng sạch. Đang dọn dẹp trước khi mở luồng mới...");
            this.localMediaStream.getTracks().forEach(track => track.stop());
            this.localMediaStream = null;
        }

        return new Promise((resolve) => {
            const video = document.getElementById(videoElementId);
            if (!video) {
                this.log("error", `❌ Không tìm thấy thẻ video với ID: ${videoElementId}`);
                resolve({ success: false, message: "Không tìm thấy thẻ video" });
                return;
            }

            video.setAttribute("playsinline", "true");
            video.setAttribute("webkit-playsinline", "true");
            video.setAttribute("muted", "true");
            video.autoplay = true;

            const constraints = {
                video: { facingMode: "user", width: { ideal: 640 }, height: { ideal: 480 } },
                audio: false
            };

            this.log("info", "📸 Đang yêu cầu trình duyệt cấp quyền Camera (getUserMedia)...");
            navigator.mediaDevices.getUserMedia(constraints)
                .then((stream) => {
                    this.log("info", "✅ Trình duyệt đã cấp quyền truy cập Camera thành công.");
                    this.localMediaStream = stream;
                    video.srcObject = stream;

                    video.onloadedmetadata = () => {
                        this.log("info", "🎬 Metadata camera đã nạp xong, tiến hành phát stream...");
                        video.play()
                            .then(() => this.log("info", "🚀 [Đồng bộ] Camera phát thành công."))
                            .catch(err => this.log("error", "❌ Lỗi tự động phát video:", err));
                    };

                    // 📌 THIẾT LẬP MUTATIONOBSERVER: 
                    // Tự động tắt Camera chủ động ngay khi thẻ video bị gỡ khỏi giao diện (DOM)
                    if (this.domObserver) {
                        this.domObserver.disconnect();
                    }
                    this.domObserver = new MutationObserver(() => {
                        if (!document.getElementById(videoElementId)) {
                            this.log("warn", `🚨 Phát hiện thẻ video [${videoElementId}] đã bị xóa khỏi DOM (Do chuyển trang)! Khởi động quy trình dọn dẹp khẩn cấp...`);
                            this.stopCamera(videoElementId);
                        }
                    });
                    this.domObserver.observe(document.body, { childList: true, subtree: true });

                    resolve({ success: true, message: "Bật camera thành công" });
                })
                .catch((err) => {
                    this.log("error", "❌ Lỗi nghiêm trọng khi gọi getUserMedia:", err);
                    resolve({ success: false, message: err.message });
                });
        });
    },

    captureAndCompress: function (videoId) {
        this.log("info", `👉 Thực hiện hành động chụp ảnh từ thẻ video: [${videoId}]`);
        const video = document.getElementById(videoId);
        if (!video) {
            this.log("error", `❌ Chụp ảnh thất bại: Không tìm thấy thẻ video với ID [${videoId}]`);
            return null;
        }
        if (video.readyState < 2) {
            this.log("warn", `⚠️ Chụp ảnh thất bại: Thẻ video chưa sẵn sàng dữ liệu hình ảnh (readyState: ${video.readyState})`);
            return null;
        }

        const canvas = document.createElement("canvas");
        const maxSize = 640;
        let w = video.videoWidth;
        let h = video.videoHeight;

        this.log("info", `📸 Kích cỡ gốc video nhận từ phần cứng: ${w}x${h}`);
        if (!w || !h) return null;

        if (w > h && w > maxSize) {
            h *= maxSize / w;
            w = maxSize;
        } else if (h > maxSize) {
            w *= maxSize / h;
            h = maxSize;
        }

        canvas.width = w;
        canvas.height = h;

        const ctx = canvas.getContext("2d");
        ctx.drawImage(video, 0, 0, w, h);

        this.log("info", `📉 Tiến hành nén ảnh JPEG chất lượng 0.6 về kích cỡ: ${w}x${h}`);
        const dataUrl = canvas.toDataURL("image/jpeg", 0.6);
        this.log("info", `✅ Chụp và nén thành công. Kích thước chuỗi Base64: ${dataUrl.length} ký tự.`);
        return dataUrl;
    },

    stopCamera: function (videoElementId) {
        this.log("info", `👉 Gọi stopCamera() cho thẻ video: [${videoElementId}]`);
        if (this.localMediaStream) {
            this.localMediaStream.getTracks().forEach(track => {
                track.stop();
            });
            this.localMediaStream = null;
        }
        const video = document.getElementById(videoElementId);
        if (video) {
            video.srcObject = null;
        }
        this.log("info", "🏁 Đã giải phóng hoàn toàn Camera.");
    }
};
