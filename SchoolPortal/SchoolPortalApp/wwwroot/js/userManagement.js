// User Management JavaScript

// Toggle password visibility
document.addEventListener('DOMContentLoaded', function() {
    // Toggle password visibility
    var togglePassword = document.getElementById('togglePassword');
    if (togglePassword) {
        togglePassword.addEventListener('click', function() {
            var passwordInput = document.getElementById('password');
            if (passwordInput) {
                var type = passwordInput.getAttribute('type') === 'password' ? 'text' : 'password';
                passwordInput.setAttribute('type', type);
                var icon = this.querySelector('i');
                if (icon) {
                    icon.classList.toggle('bi-eye');
                    icon.classList.toggle('bi-eye-slash');
                }
            }
        });
    }

    // Generate random password
    var generatePasswordBtn = document.getElementById('generatePassword');
    if (generatePasswordBtn) {
        generatePasswordBtn.addEventListener('click', function(e) {
            e.preventDefault();
            var specialChars = '!@#$%^&*()';
            var chars = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789' + specialChars;
            var password = '';
            for (var i = 0; i < 12; i++) {
                password += chars.charAt(Math.floor(Math.random() * chars.length));
            }
            var passwordInput = document.getElementById('password');
            if (passwordInput) {
                passwordInput.value = password;
            }
        });
    }

    // Generate username from name
    var generateUsernameBtn = document.getElementById('generateUsername');
    if (generateUsernameBtn) {
        generateUsernameBtn.addEventListener('click', function(e) {
            e.preventDefault();
            var firstName = document.getElementById('firstName');
            var lastName = document.getElementById('lastName');
            var username = document.getElementById('username');
            
            if (firstName && lastName && username) {
                var firstNameVal = firstName.value ? firstName.value.toLowerCase() : '';
                var lastNameVal = lastName.value ? lastName.value.toLowerCase() : '';
                
                if (firstNameVal && lastNameVal) {
                    var randomNum = Math.floor(Math.random() * 100);
                    username.value = firstNameVal.charAt(0) + lastNameVal + randomNum;
                }
            }
        });
    }

    // Initialize tooltips
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.forEach(function (tooltipTriggerEl) {
        new bootstrap.Tooltip(tooltipTriggerEl);
    });

    // Form submission
    var addUserForm = document.getElementById('addUserForm');
    if (addUserForm) {
        addUserForm.addEventListener('submit', function(e) {
            e.preventDefault();
            // Add your form submission logic here
            var modal = bootstrap.Modal.getInstance(document.getElementById('addUserModal'));
            if (modal) {
                modal.hide();
            }
            // Show success message
            alert('User created successfully!');
        });
    }
});
