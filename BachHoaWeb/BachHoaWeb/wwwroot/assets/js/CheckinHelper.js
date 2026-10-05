let currentStream = null;
let gpsWatchId = null;
let gpsHistory = [];

window.checkInHelper = {
    gpsWatchId: null,
    lastRecordedLocation: null,
    localMediaStream: null,
    domObserver: null,

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
        window.checkInHelper.log("info", "👉 Gọi getImmediateLocation()");
        if (window.checkInHelper.lastRecordedLocation !== null) {
            window.checkInHelper.log("info", "📍 Sử dụng vị trí lưu tạm có sẵn:", window.checkInHelper.lastRecordedLocation);
            return window.checkInHelper.lastRecordedLocation;
        }

        return new Promise((resolve) => {
            window.checkInHelper.log("info", "📡 Bắt đầu quét vị trí bằng getCurrentPosition...");
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    let currentPos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                        accuracy: position.coords.accuracy,
                        isFake: false,
                        fakeReason: ""
                    };
                    window.checkInHelper.lastRecordedLocation = currentPos;
                    window.checkInHelper.log("info", "✅ Lấy vị trí thành công (getImmediateLocation):", currentPos);
                    resolve(currentPos);
                },
                function (error) {
                    window.checkInHelper.log("error", "❌ Lỗi getCurrentPosition trong getImmediateLocation:", error);
                    resolve(null);
                },
                { enableHighAccuracy: true, timeout: 4000, maximumAge: 0 }
            );
        });
    },

    startGpsWatching: function () {
        window.checkInHelper.log("info", "👉 Gọi startGpsWatching()");
        if (window.checkInHelper.gpsWatchId !== null) {
            window.checkInHelper.log("warn", `⚠️ gpsWatchId (${window.checkInHelper.gpsWatchId}) đã tồn tại. Tiến hành xóa để làm mới...`);
            navigator.geolocation.clearWatch(window.checkInHelper.gpsWatchId);
        }

        window.checkInHelper.gpsWatchId = navigator.geolocation.watchPosition(
            function (position) {
                window.checkInHelper.lastRecordedLocation = {
                    lat: position.coords.latitude,
                    lng: position.coords.longitude,
                    accuracy: position.coords.accuracy,
                    isFake: false,
                    fakeReason: ""
                };
                window.checkInHelper.log("info", "🛰️ [WatchGPS] Vị trí cập nhật liên tục:", window.checkInHelper.lastRecordedLocation);
            },
            function (error) {
                window.checkInHelper.log("error", "❌ [WatchGPS] Lỗi theo dõi vị trí liên tục:", error);
            },
            { enableHighAccuracy: true, timeout: 10000, maximumAge: 0 }
        );
        window.checkInHelper.log("info", `✅ Đã thiết lập định vị liên tục với ID: ${window.checkInHelper.gpsWatchId}`);
    },

    stopGpsWatching: function () {
        window.checkInHelper.log("info", "👉 Gọi stopGpsWatching()");
        if (window.checkInHelper.gpsWatchId !== null) {
            navigator.geolocation.clearWatch(window.checkInHelper.gpsWatchId);
            window.checkInHelper.log("info", `⚙️ Đã clearWatch thành công ID: ${window.checkInHelper.gpsWatchId}`);
            window.checkInHelper.gpsWatchId = null;
            window.checkInHelper.lastRecordedLocation = null;
        } else {
            window.checkInHelper.log("warn", "⚠️ Không có gpsWatchId nào đang chạy để dừng.");
        }
    },

    getLocationAndVerify: function () {
        window.checkInHelper.log("info", "👉 Gọi getLocationAndVerify()");
        return new Promise((resolve, reject) => {
            if (window.checkInHelper.lastRecordedLocation !== null) {
                window.checkInHelper.log("info", "📍 Trả về vị trí lưu tạm ngay lập tức:", window.checkInHelper.lastRecordedLocation);
                return resolve(window.checkInHelper.lastRecordedLocation);
            }

            window.checkInHelper.log("warn", "⚠️ Chưa có vị trí lưu tạm, tiến hành quét cưỡng bức getCurrentPosition...");

            navigator.geolocation.getCurrentPosition(
                function (position) {
                    window.checkInHelper.lastRecordedLocation = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude,
                        accuracy: position.coords.accuracy,
                        isFake: false,
                        fakeReason: ""
                    };
                    window.checkInHelper.log("info", "✅ Quét cưỡng bức thành công:", window.checkInHelper.lastRecordedLocation);
                    resolve(window.checkInHelper.lastRecordedLocation);
                },
                function (error) {
                    let msg = "Chưa thu thập được dữ liệu vị trí. ";
                    if (error.code === 1) msg += "Người dùng từ chối cấp quyền.";
                    else if (error.code === 2) msg += "Không có tín hiệu GPS (Hãy bật Vị trí thiết bị).";
                    else if (error.code === 3) msg += "Hết thời gian tìm tín hiệu GPS.";

                    window.checkInHelper.log("error", `❌ Quét cưỡng bức thất bại: ${msg}`, error);
                    reject(new Error(msg));
                },
                { enableHighAccuracy: true, timeout: 5000, maximumAge: 0 }
            );
        });
    },

    getLocation: async function () {
        window.checkInHelper.log("info", "👉 Bắt đầu vòng lặp getLocation() tối ưu độ chính xác (Thử tối đa 5 lần)");
        let bestPos = null;
        for (let i = 0; i < 5; i++) {
            window.checkInHelper.log("info", `🔄 Lần thử quét vị trí thứ ${i + 1}/5...`);
            try {
                const pos = await new Promise((resolve, reject) => {
                    navigator.geolocation.getCurrentPosition(resolve, reject, {
                        enableHighAccuracy: true,
                        timeout: 5000,
                        maximumAge: 0
                    });
                });

                window.checkInHelper.log("info", `🎯 Lần thử ${i + 1} thành công. Độ chính xác hiện tại: ${pos.coords.accuracy}m`);

                if (!bestPos || pos.coords.accuracy < bestPos.coords.accuracy) {
                    bestPos = pos;
                }

                if (pos.coords.accuracy <= 20) {
                    window.checkInHelper.log("info", "🎯 Độ chính xác đã đạt yêu cầu (<= 20m). Dừng vòng lặp sớm.");
                    break;
                }
            } catch (err) {
                window.checkInHelper.log("warn", `⚠️ Lỗi xảy ra tại lần thử ${i + 1}:`, err);
            }
            await new Promise(r => setTimeout(r, 800));
        }

        if (!bestPos) {
            window.checkInHelper.log("error", "❌ Qua 5 lần thử đều không lấy được vị trí nào hợp lệ.");
            throw new Error("Không lấy được vị trí");
        }

        const result = {
            lat: bestPos.coords.latitude,
            lng: bestPos.coords.longitude,
            accuracy: bestPos.coords.accuracy
        };
        window.checkInHelper.log("info", "✅ Vị trí tốt nhất tìm được:", result);
        return result;
    },

    // ================= CLIENT CLOCK =================
    initClientClock: function (initialServerTimeTicks, elementId) {
        window.checkInHelper.log("info", `👉 Khởi tạo initClientClock cho Element ID: [${elementId}]`);
        const el = document.getElementById(elementId);
        if (!el) {
            window.checkInHelper.log("error", `❌ Không tìm thấy phần tử HTML hiển thị đồng hồ với ID: ${elementId}`);
            return;
        }

        let serverTimeMs = (initialServerTimeTicks - 621355968000000000) / 10000;

        if (window.clientClockInterval) {
            window.checkInHelper.log("warn", "⚠️ Đồng hồ cũ đang chạy, tiến hành Clear Interval cũ.");
            clearInterval(window.clientClockInterval);
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

        window.clientClockInterval = setInterval(() => {
            serverTimeMs += 1000;
            const date = new Date(serverTimeMs);
            if (document.getElementById(elementId)) {
                document.getElementById(elementId).innerText = formatter.format(date).replace(',', '');
            } else {
                window.checkInHelper.log("warn", `⚠️ Phần tử [${elementId}] đã biến mất khỏi DOM. Tự động hủy Interval đồng hồ.`);
                clearInterval(window.clientClockInterval);
                window.clientClockInterval = null;
            }
        }, 1000);
        window.checkInHelper.log("info", "✅ Kích hoạt đồng hồ đếm giây thành công.");
    },

    // ================= CAMERA METHODS =================
    startCamera: function (videoElementId) {
        window.checkInHelper.log("info", `👉 Khởi chạy startCamera() cho thẻ video: [${videoElementId}]`);
        return new Promise((resolve) => {
            let video = document.getElementById(videoElementId);
            if (!video) {
                window.checkInHelper.log("error", `❌ Không tìm thấy thẻ video với ID: ${videoElementId}`);
                resolve({ success: false, message: "Không tìm thấy thẻ video" });
                return;
            }

            video.setAttribute("playsinline", "true");
            video.setAttribute("webkit-playsinline", "true");
            video.setAttribute("muted", "true");
            video.autoplay = true;

            let constraints = {
                video: { facingMode: "user", width: { ideal: 640 }, height: { ideal: 480 } },
                audio: false
            };

            window.checkInHelper.log("info", "📸 Đang yêu cầu trình duyệt cấp quyền Camera (getUserMedia)...");
            navigator.mediaDevices.getUserMedia(constraints)
                .then(function (stream) {
                    window.checkInHelper.log("info", "✅ Trình duyệt đã cấp quyền truy cập Camera thành công.");
                    window.checkInHelper.localMediaStream = stream;
                    video.srcObject = stream;

                    video.onloadedmetadata = function () {
                        window.checkInHelper.log("info", "🎬 Metadata camera đã nạp xong, tiến hành phát stream...");
                        video.play()
                            .then(() => window.checkInHelper.log("info", "🚀 [Đồng bộ] Camera phát thành công."))
                            .catch(err => window.checkInHelper.log("error", "❌ Lỗi tự động phát video:", err));
                    };

                    resolve({ success: true, message: "Bật camera thành công" });
                })
                .catch(function (err) {
                    window.checkInHelper.log("error", "❌ Lỗi nghiêm trọng khi gọi getUserMedia:", err);
                    resolve({ success: false, message: err.message });
                });
        });
    },

    captureAndCompress: (videoId) => {
        window.checkInHelper.log("info", `👉 Thực hiện hành động chụp ảnh từ thẻ video: [${videoId}]`);
        const video = document.getElementById(videoId);
        if (!video) {
            window.checkInHelper.log("error", `❌ Chụp ảnh thất bại: Không tìm thấy thẻ video với ID [${videoId}]`);
            return null;
        }
        if (video.readyState < 2) {
            window.checkInHelper.log("warn", `⚠️ Chụp ảnh thất bại: Thẻ video chưa sẵn sàng dữ liệu hình ảnh (readyState: ${video.readyState})`);
            return null;
        }

        const canvas = document.createElement("canvas");
        const maxSize = 640;
        let w = video.videoWidth;
        let h = video.videoHeight;

        window.checkInHelper.log("info", `📸 Kích cỡ gốc video nhận từ phần cứng: ${w}x${h}`);
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

        window.checkInHelper.log("info", `📉 Tiến hành nén ảnh JPEG chất lượng 0.6 về kích cỡ: ${w}x${h}`);
        const dataUrl = canvas.toDataURL("image/jpeg", 0.6);
        window.checkInHelper.log("info", `✅ Chụp và nén thành công. Kích thước chuỗi Base64: ${dataUrl.length} ký tự.`);
        return dataUrl;
    },

    stopCamera: function (videoElementId) {
        window.checkInHelper.log("info", `👉 Gọi stopCamera() cho thẻ video: [${videoElementId}]`);
        if (window.checkInHelper.localMediaStream) {
            window.checkInHelper.localMediaStream.getTracks().forEach(track => {
                window.checkInHelper.log("info", `🛑 Đang tắt Track phần cứng: Mác loại [${track.kind}], Nhãn [${track.label}]`);
                track.stop();
            });
            window.checkInHelper.localMediaStream = null;
        }

        let video = document.getElementById(videoElementId);
        if (video) {
            video.srcObject = null;
            window.checkInHelper.log("info", `⚙️ Đã gỡ bỏ srcObject của thẻ video [${videoElementId}].`);
        }

        if (window.checkInHelper.domObserver) {
            window.checkInHelper.domObserver.disconnect();
            window.checkInHelper.domObserver = null;
            window.checkInHelper.log("info", "⚙️ Đã ngắt MutationObserver.");
        }
        window.checkInHelper.log("info", "🏁 Đã giải phóng hoàn toàn Camera.");
    }
};