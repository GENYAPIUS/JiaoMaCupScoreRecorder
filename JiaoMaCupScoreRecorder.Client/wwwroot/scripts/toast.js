function showToast(message) {
    const toastElements = [].slice.call(document.querySelectorAll(".toast"));
    const toasts = toastElements.map((toastElement) => {
        return new bootstrap.Toast(toastElement);
    });
    const toastContent = document.querySelector(".toast-body");
    toastContent.innerHTML = `有重複的圖片。<br>${message}`;
    toasts.forEach(toast => toast.show());
}

function closeToast() {
    const toastElements = [].slice.call(document.querySelectorAll(".toast"));
    const toasts = toastElements.map((toastElement) => {
        return new bootstrap.Toast(toastElement);
    });
    toasts.forEach(toast => toast.hide());
}