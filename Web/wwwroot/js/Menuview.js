function toggleIconPosition(condition) {
    var containers = document.querySelectorAll('.search-container');
    containers.forEach(function (container) {
        container.classList.remove('left-icon', 'right-icon');
        if (condition === 'left') {
            container.classList.add('left-icon');
        } else if (condition === 'right') {
            container.classList.add('right-icon');
        }
    });
}
// For toggle the side tabs of menu categories and modifier groups
function toggleTabs() {
    var sideTabs = document.querySelectorAll(".side-tab");
    sideTabs.forEach(function (tab) {
        if (tab.style.display === "none" || tab.style.display === "") {
            tab.style.display = "block";
        } else {
            tab.style.display = "none";
        }
    });
}

// For menu items all check
const mainCheck = document.getElementById("flexCheckDefault").addEventListener("change", function () {
    let chechBoxes = document.querySelectorAll(".cb");
    chechBoxes.forEach((checkBox) => {
        checkBox.checked = this.checked;
    })
})  
// var abc = document.getElementById("ModifierGroupId");
// var abcd = document.getElementById("categoryId");

// Get the data for Edit Modifier Group
var editButtonsModifiers = document.querySelectorAll('.btn-edit-modifier');
editButtonsModifiers.forEach(function (button) {
    button.addEventListener('click', function () {
        var ModifierGroupId = button.getAttribute('data-id');
        var modifierGroupName = button.getAttribute('data-name');
        var modifierGroupDescription = button.getAttribute('data-description');
        console.log(ModifierGroupId);
        console.log(modifierGroupName);
        console.log(modifierGroupDescription);

        var modal = document.getElementById('editModifierGroup_' + ModifierGroupId);
        modal.querySelector('[name="ModifierGroupId"]').value = ModifierGroupId;
        modal.querySelector('[name="modifierGroupName"]').value = modifierGroupName;
        modal.querySelector('[name="modifierGroupDescription"]').value = modifierGroupDescription;
    });
});

// Post method for edit modifier group
// $('body').on('submit', '#edimodifiergroup', function (e) {
//     e.preventDefault();
//     var formData = $(this).serialize();
//     $.ajax({
//         url: '@Url.Action("EditModifierGroup", "Menu")',
//         type: 'POST',
//         data: formData,
//         success: function (response) {
//             if (response.success) {
//                 $('#EditItemModal').modal('hide');
//                 $('.modal-backdrop').remove();
//                 $('body').removeClass('modal-open');
//             }
//         },
//         error: function (xhr, status, error) {
//             console.log('Error:', error);
//             alert('There was an error updating the item.');
//         }
//     });
// });

// var deleteButtons = document.querySelectorAll('.btn-delete');
// deleteButtons.forEach(function (button) {
//     button.addEventListener('click', function () {
//         var categoryId = button.getAttribute('data-id');
//         document.getElementById('categoryIdToDelete').value = categoryId;
//     });
// });


// For Showing items according to category
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".cat").forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            let categoryId = this.dataset.categoryId;
            fetch(`/Menu/GetItemsByCategory?categoryid=${categoryId}`)
                .then(response => response.text())
                .then(data => {
                    document.getElementById("ItemListContainer").innerHTML = data;
                })
                .catch(error => console.error("Error loading items:", error));
        });
    });
});

// For Showing modifiers according to modifier group
document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".modifiergroup").forEach(button => {
        button.addEventListener("click", function (e) {
            e.preventDefault();
            let modifierId = this.dataset.modifierId;
            fetch(`/Menu/GetModifiersByModifierGroup?modifierid=${categoryId}`)
                .then(response => response.text())
                .then(data => {
                    document.getElementById("ItemListContainer").innerHTML = data;
                })
                .catch(error => console.error("Error loading items:", error));
        });
    });
});


// For highlight the text of the selected link
document.addEventListener('DOMContentLoaded', function () {
    const categoryLinks = document.querySelectorAll('.cat');
    categoryLinks.forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            categoryLinks.forEach(link => link.classList.remove('active-link'));
            this.classList.add('active-link');
        });
    });
    if (categoryLinks.length > 0) {
        categoryLinks[0].classList.add('active-link');
    }
});


// function openEditModal(itemId) {
//     $.ajax({
//         url: '@Url.Action("EditMenuItem", "Menu")', 
//         method: 'GET',
//         data: { id: itemId }, 
//         success: function (data) {
//             $('#EditItemModal .modal-body').html(data);
//             var myModal = new bootstrap.Modal(document.getElementById('EditItemModal'));
//             myModal.show();
//         },
//         error: function () {
//             alert('Error loading item details.');
//         }
//     });
// }


// $(document).ready(function () {
//     // Trigger the AJAX call when the edit link is clicked
//     var abcde = document.getElementById("editModifierGroup_"+ ModifierGroupId);
//     abcde.on('click', function (e) {
//         e.preventDefault();
//         var itemId = $(this).data('id');
//         $.ajax({
//             url: '@Url.Action("EditModifierGroup", "Menu")',
//             type: 'GET',
//             data: { id: 1 },
//             success: function (response) {
//                 $('#EditItemModal .modal-content').html(response);
//                 $('#EditItemModal').modal('show');
//             },
//             error: function (xhr, status, error) {
//                 console.log('Error:', error);
//                 alert('There was an error loading the item data.');
//             }
//         });
//     });
    // $('body').on('click', '.', function (e) {
    //     e.preventDefault();
    //     var itemId = $(this).data('id');
    //     $.ajax({
    //         url: '@Url.Action("EditMenuItem", "Menu")',
    //         type: 'GET',
    //         data: { id: itemId },
    //         success: function (response) {
    //             $('#EditItemModal .modal-content').html(response);
    //             $('#EditItemModal').modal('show');
    //         },
    //         error: function (xhr, status, error) {
    //             console.log('Error:', error);
    //             alert('There was an error loading the item data.');
    //         }
    //     });
    // });
// });