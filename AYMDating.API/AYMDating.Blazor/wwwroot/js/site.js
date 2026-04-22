window.focusHelper = {
    setFocus: function (element) {
        element.focus();
    }
};


function enforceMaxLength(element, maxLength) {
    if (element.value.length > maxLength) {
        element.value = element.value.slice(0, maxLength);
    }
}

function showConfirmation(message) {
    return confirm(message);
}