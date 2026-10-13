window.showAlert = function (message) {
    alert(message);
}
window.togglePassword = function () {
    const passInput = document.getElementById('passwordField');
    if (passInput) {
        passInput.type = passInput.type === 'password' ? 'text' : 'password';
    }
}
window.focusElementById = (id) => {
    const el = document.getElementById(id);
    if (el) {
        el.focus();
        el.select?.(); // optional: bôi đen text luôn
    }
};