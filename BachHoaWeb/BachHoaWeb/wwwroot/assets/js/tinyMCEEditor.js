
if (!window.destroyTinyMCE) {
    window.destroyTinyMCE = function (id) {
        const editor = tinymce.get(id);
        if (editor) {
            editor.remove();
        }
    };
}

// --- INIT ---
if (!window.initTinyMCE) {
    window.initTinyMCE = function (id, dotnetHelper, initialValue) {

        // Destroy nếu đã tồn tại
        const existing = tinymce.get(id);
        if (existing) {
            existing.remove();
        }

        tinymce.init({
            selector: `#${id}`,
            height: 500,
            menubar: false,
            branding: false,
            plugins: 'image code link lists advlist emoticons charmap table',
            toolbar:
                'undo redo | fontfamily fontsize | bold italic underline strikethrough | ' +
                'forecolor backcolor | link image table | align lineheight | numlist bullist indent outdent | emoticons charmap | removeformat',

            // ✅ Giữ giao diện upload "1 tab" (General có icon upload)
            image_uploadtab: false,
            file_picker_types: 'image',

            // ✅ Upload ảnh qua API backend
            images_upload_handler: function (blobInfo) {
                return new Promise((resolve, reject) => {
                    const formData = new FormData();
                    formData.append("file", blobInfo.blob(), blobInfo.filename());

                    fetch("/api/article/upload-temp", {
                        method: "POST",
                        body: formData
                    })
                        .then(r => {
                            if (!r.ok) throw new Error("Upload failed");
                            return r.json();
                        })
                        .then(result => {
                            const url = result.location || result.url;
                            if (!url) throw new Error("No URL returned");
                            resolve(url);
                        })
                        .catch(err => {
                            console.error("Upload error:", err);
                            reject(err.message);
                        });
                });
            },

            // ✅ Tích hợp upload ngay trong dialog "Insert/Edit Image"
            file_picker_callback: (cb, value, meta) => {
                if (meta.filetype === 'image') {
                    const input = document.createElement('input');
                    input.type = 'file';
                    input.accept = 'image/*';
                    input.onchange = function () {
                        const file = this.files[0];
                        const reader = new FileReader();

                        reader.onload = function () {
                            const id = 'blobid' + (new Date()).getTime();
                            const blobCache = tinymce.activeEditor.editorUpload.blobCache;
                            const base64 = reader.result.split(',')[1];
                            const blobInfo = blobCache.create(id, file, base64);
                            blobCache.add(blobInfo);
                            cb(blobInfo.blobUri(), { title: file.name });
                        };
                        reader.readAsDataURL(file);
                    };
                    input.click();
                }
            },

            setup: function (editor) {
                // 🔥 Đồng bộ nội dung về Blazor
                editor.on('change keyup', function () {
                    dotnetHelper.invokeMethodAsync("OnContentChanged", editor.getContent());
                });

                // ✅ Set nội dung khởi tạo
                editor.on('init', function () {
                    if (initialValue) {
                        editor.setContent(initialValue);
                    }
                });
            }
        });
    };
}
