window.onload = () => {
    let authorSelectionRadios = document.querySelectorAll('input[name="add-new-author"]');
    let authorTextInput = document.querySelector('#author-text-input');
    let authorDropdown = document.querySelector('#author-dropdown');

    for (let radio of authorSelectionRadios) {
        radio.addEventListener('change', () => {
            console.log(radio.value);
            if (radio.value === 'true') {
                authorTextInput.classList.remove('hidden');
                authorDropdown.classList.add('hidden');
            }
            else {
                authorTextInput.classList.add('hidden');
                authorDropdown.classList.remove('hidden');
            }
        });
    }
};