window.vnDateInput = {
    setValue: function (el, value) {
        const start = el.selectionStart;
        const oldLength = el.value.length;
        el.value = value;
        const newLength = value.length;
        const diff = newLength - oldLength;
        let pos = start + diff;
        if (pos < 0) pos = 0;
        try {
            el.setSelectionRange(pos, pos);
        } catch { }
    },

    selectAll: function (el) {
        setTimeout(() => {
            el.select();
        }, 0);
    },

    // THÀNH PHẦN THÊM MỚI: Bộ gác cổng chặn chữ và bắt dấu phân cách
    setupMask: function (el) {
        if (!el) return;

        // 1. Chặn gõ chữ trực tiếp từ bàn phím
        el.addEventListener('keypress', function (e) {
            // Cho phép các tổ hợp phím hệ thống (Ctrl+A, Ctrl+C, v.v...) hoạt động bình thường
            if (e.ctrlKey || e.metaKey || e.altKey) return;

            // Nếu người dùng gõ dấu phân cách, ta cho phép nhưng xử lý riêng ở keydown để nhảy ô
            if (['/', '-', '.', ':'].includes(e.key)) {
                return;
            }

            // Chỉ chấp nhận các chữ số từ 0 đến 9
            if (!/[0-9]/.test(e.key)) {
                e.preventDefault(); // Nuốt phím chữ ngay lập tức
            }
        });

        // 2. Chặn bọc đầu khi người dùng chuột phải rồi Paste chuỗi có chữ
        el.addEventListener('paste', function (e) {
            const clipboardData = e.clipboardData || window.clipboardData;
            const pastedData = clipboardData.getData('Text');

            // Nếu chuỗi tính dán vào có chứa bất kỳ ký tự nào không phải số -> Hủy lệnh dán
            if (/[^\d]/.test(pastedData)) {
                e.preventDefault();
            }
        });
    }
};