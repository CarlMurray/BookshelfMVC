let editBookDialog = document.getElementById('edit-book-dialog');
let editBookBtn = document.getElementById('edit-book-btn');
let deleteBookBtn = document.getElementById('delete-book-btn');
let buttons = [editBookBtn, deleteBookBtn];
let action;


buttons.forEach(button => {
    button.addEventListener('click', function () {
        action = this.getAttribute('data-action');
        let dialogHeader = document.querySelector('#edit-book-dialog-header');
        let dialogForm = document.querySelector('#edit-book-dialog-form');
        dialogHeader.innerText = `Choose a book to ${action}`
        dialogForm.setAttribute('action', `books/${action}Book`)
        if (action === 'edit') {
            dialogForm.setAttribute('method', 'get');
        }
        else dialogForm.setAttribute('method', 'post');

        editBookDialog.showModal();
    });
}
)