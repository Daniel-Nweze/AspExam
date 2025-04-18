// ===========================
// MODAL-HANTERING
// ===========================

document.addEventListener('click', (e) => {
    const openModalBtn = e.target.closest('[data-type="modal"]');
    const closeModalBtn = e.target.closest('[data-type="close"]');

    if (openModalBtn) {
        const targetId = openModalBtn.getAttribute('data-target');
        const modal = document.querySelector(targetId);
        if (modal) modal.classList.add('modal-show');
    }

    if (closeModalBtn) {
        const targetId = closeModalBtn.getAttribute('data-target');
        const modal = document.querySelector(targetId);
        if (modal) modal.classList.remove('modal-show');
    }
});

// ===========================
// DROPDOWN-HANTERING
// ===========================

// Automatisk ID-generering för dropdowns
document.querySelectorAll('[data-type="dropdown"]').forEach((btn, index) => {
    const parent = btn.closest('[id*="actions-container"]') || btn.parentElement;
    const dropdown = parent.querySelector('.dropdown');
    const uniqueId = `dropdown-${index}-${Math.random().toString(36).substring(2, 6)}`;
    if (dropdown) {
        dropdown.id = uniqueId;
        btn.setAttribute('data-target', `#${uniqueId}`);
    }
});

document.addEventListener('click', (e) => {
    const clickedButton = e.target.closest('[data-type="dropdown"]');
    const allDropdowns = document.querySelectorAll('.dropdown');

    if (clickedButton) {
        const targetId = clickedButton.getAttribute('data-target');
        const targetDropdown = document.querySelector(targetId);

        allDropdowns.forEach(drop => {
            if (drop !== targetDropdown) {
                drop.classList.remove('dropdown-show');
                drop.classList.add('hide');
            }
        });

        if (targetDropdown) {
            targetDropdown.classList.toggle('dropdown-show');
            targetDropdown.classList.toggle('hide');
        }
    } else if (!e.target.closest('.dropdown')) {
        // Klick utanför stänger alla öppna dropdowns
        allDropdowns.forEach(drop => {
            drop.classList.remove('dropdown-show');
            drop.classList.add('hide');
        });
    }
});

// ===========================
// FORM SELECT DROPDOWNS
// ===========================

document.querySelectorAll('.form-select').forEach(select => {
    const trigger = select.querySelector('.form-select-trigger');
    const triggerText = trigger.querySelector('.form-select-text');
    const options = select.querySelectorAll('.form-select-option');
    const hiddenInput = select.querySelector('input[type="hidden"]');
    const placeholder = select.dataset.placeholder || "Choose";

    const setValue = (value = "", text = placeholder) => {
        triggerText.textContent = text;
        hiddenInput.value = value;
        select.classList.toggle('has-placeholder', !value);
    };

    setValue();

    trigger.addEventListener('click', (e) => {
        e.stopPropagation();
        document.querySelectorAll('.form-select.open').forEach(openSelect => {
            if (openSelect !== select) openSelect.classList.remove('open');
        });
        select.classList.toggle('open');
    });

    options.forEach(option => {
        option.addEventListener('click', () => {
            setValue(option.dataset.value, option.textContent);
            select.classList.remove('open');
        });
    });

    document.addEventListener('click', e => {
        if (!select.contains(e.target)) {
            select.classList.remove('open');
        }
    });
});
