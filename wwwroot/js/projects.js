function toggleDropdown(projectId) {
    const el = document.getElementById(`dropdown-${projectId}`);
    el.classList.toggle("dropdown-show");
    document.addEventListener("click", function handler(e) {
        if (!el.contains(e.target)) {
            el.classList.remove("dropdown-show");
            document.removeEventListener("click", handler);
        }
    });
}

function toggleDropdown(id) {
    const dropdown = document.getElementById(`dropdown-${id}`);
    dropdown.classList.toggle('show');
}