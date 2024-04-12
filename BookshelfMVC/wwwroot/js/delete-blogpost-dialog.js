const cancelBtn = document.querySelector('#modal-close-btn');
const deletePostBtn = document.querySelector('#delete-post-btn');
const modal = document.querySelector('dialog');
deletePostBtn.addEventListener('click',() => modal.showModal())
cancelBtn.addEventListener('click', () => modal.close())