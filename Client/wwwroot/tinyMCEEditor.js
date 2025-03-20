window.initializeTinyMCE = function () {
    tinymce.init({
        selector: '#tinymce-editor',
        plugins: 'lists link image table',
        toolbar: 'undo redo | bold italic underline | bullist numlist | link image',
        menubar: false,
        height: 300
    });
};

window.getTinyMCEContent = function () {
    return tinymce.get('tinymce-editor').getContent();
};
