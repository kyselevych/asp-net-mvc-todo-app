function handleClickErrorBtn(e) {
    const target = e.target;

    if (!target.classList.contains('control--delete')) return;

    const message = "Are you sure you want to remove the task?";
    const isConfirm = confirm(message);

    if (!isConfirm) {
        e.preventDefault();
        return;
    }
}

document.addEventListener('click', handleClickErrorBtn);
