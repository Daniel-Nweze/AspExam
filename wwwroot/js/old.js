//const dropdowns = document.querySelectorAll('[data-type="dropdown"]')

//document.addEventListener('click', function (event) {
//    let clickedDropdown = null

//    dropdowns.forEach(dropdown => {
//        const targetId = dropdown.getAttribute('data-target')
//        const targetElement = document.querySelector(targetId)

//        if (dropdown.contains(event.target)) {
//            clickedDropdown = targetElement


//            document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
//                if (openDropdown !== targetElement) {
//                    openDropdown.classList.remove("dropdown-show")
//                }

//            })

//            targetElement.classList.toggle('dropdown-show')
//        }
//    })

//    if (!clickedDropdown && !event.target.closest('.dropdown')) {
//        document.querySelectorAll('.dropdown.dropdown-show').forEach(openDropdown => {
//            openDropdown.classList.remove('dropdown-show')
//        })

//    }
//})


//const modals = document.querySelectorAll('[data-type="modal"]')
//console.log(modals)

//document.addEventListener('click', function (event) {
//    let clickedModal = null

//    modals.forEach(modal => {
//        const targetId = modal.getAttribute('data-target')
//        const targetElement = document.querySelector(targetId)

//        if (modal.contains(event.target)) {
//            clickedModal = targetElement


//            document.querySelectorAll('.modal.modal-show').forEach(openModal => {
//                if (openModal !== targetElement) {
//                    openModal.classList.remove("modal-show")
//                }

//            })

//            targetElement.classList.toggle('modal-show')
//        }
//    })

//    if (!clickedModal && !event.target.closest('.modal')) {
//        document.querySelectorAll('.modal.modal-show').forEach(openModal => {
//            openModal.classList.remove('modal-show')
//        })

//    }
//})




