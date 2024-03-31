let editBookDialog = document.getElementById('edit-book-dialog');
let editBookBtn = document.getElementById('edit-book-btn');
let deleteBookBtn = document.getElementById('delete-book-btn');
let buttons = [editBookBtn, deleteBookBtn];
let action;


buttons.forEach(button => {
    button.addEventListener('click', function () {
        action = this.getAttribute('data-action');
        console.log(action);
        let dialogHeader = document.querySelector('#edit-book-dialog-header');
        dialogHeader.innerText = `Choose a book to ${action}`
        let dialogForm = document.querySelector('#edit-book-dialog-form');
        dialogForm.setAttribute('action', 'DeleteBook')
        editBookDialog.showModal();
    });
}
)