function showToast() {
    console.log("showToast called.");
    const toastElements = [].slice.call(document.querySelectorAll(".toast"));
    toastElements.forEach(toastElement => console.log(toastElement));
    const toasts = toastElements.map((toastElement) => {
        return new bootstrap.Toast(toastElement);
    });
    toasts.forEach(toast => toast.show());
}

function closeToast() {
    const toastElements = [].slice.call(document.querySelectorAll(".toast"));
    const toasts = toastElements.map((toastElement) => {
        return new bootstrap.Toast(toastElement);
    });
    toasts.forEach(toast => toast.hide());
}