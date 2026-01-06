// Check if the current user has a specific permission
function hasPermission(permission) {
    return window.userPermissions && window.userPermissions.includes(permission);
}

// Simple toast notification function if not already defined
if (typeof showToast === 'undefined') {
    function showToast(title, message, type = 'info') {
        console.log(`${type.toUpperCase()}: ${title} - ${message}`);
        // You can enhance this with a proper toast notification library
    }
}

// Add to your site.js or a new validation.js file
$(document).ready(function() {
    // Permission-based UI updates
    $('[data-required-permission]').each(function() {
        const $element = $(this);
        const requiredPermission = $element.data('required-permission');
        if (requiredPermission && !hasPermission(requiredPermission)) {
            $element.hide();
        }
    });

    // User form validation
    if (typeof $.fn.validate !== 'undefined') {
        $('#userForm').validate({
            rules: {
                userName: {
                    required: true,
                    minlength: 3,
                    maxlength: 50
                },
                email: {
                    required: true,
                    email: true
                },
                firstName: {
                    required: true,
                    maxlength: 50
                },
                lastName: {
                    required: true,
                    maxlength: 50
                }
            },
            messages: {
                userName: {
                    required: "Please enter a username",
                    minlength: "Username must be at least 3 characters long"
                },
                email: {
                    required: "Please enter an email address",
                    email: "Please enter a valid email address"
                },
                firstName: {
                    required: "Please enter a first name"
                },
                lastName: {
                    required: "Please enter a last name"
                }
            },
            errorElement: 'span',
            errorPlacement: function(error, element) {
                error.addClass('invalid-feedback');
                element.closest('.form-group').append(error);
            },
            highlight: function(element, errorClass, validClass) {
                $(element).addClass('is-invalid');
            },
            unhighlight: function(element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
            }
        });
    }

    // Role form validation
    if (typeof $.fn.validate !== 'undefined') {
        $('#roleForm').validate({
            rules: {
                name: {
                    required: true,
                    minlength: 3,
                    maxlength: 50
                },
                description: {
                    maxlength: 500
                }
            },
            messages: {
                name: {
                    required: "Please enter a role name",
                    minlength: "Role name must be at least 3 characters long"
                }
            },
            errorElement: 'span',
            errorPlacement: function(error, element) {
                error.addClass('invalid-feedback');
                element.closest('.form-group').append(error);
            },
            highlight: function(element, errorClass, validClass) {
                $(element).addClass('is-invalid');
            },
            unhighlight: function(element, errorClass, validClass) {
                $(element).removeClass('is-invalid');
            }
        });
    }

    // Disable form submission if user doesn't have permission
    $('form').on('submit', function() {
        const requiredPermission = $(this).data('required-permission');
        if (requiredPermission && !hasPermission(requiredPermission)) {
            showToast('Error', 'You do not have permission to perform this action.', 'error');
            return false;
        }
        return true;
    });
});
