// Healthcare Authentication JavaScript
// Create this as ~/js/auth.js

document.addEventListener('DOMContentLoaded', function () {
    initializeAuthForm();
    initializeFloatingLabels();
    autoFocusFirstInput();
});

// Initialize authentication form functionality
function initializeAuthForm() {
    const authForm = document.getElementById('account');
    const submitButton = document.getElementById('login-submit');

    if (authForm && submitButton) {
        authForm.addEventListener('submit', function (e) {
            // Check both HTML5 validation and jQuery validation (if present)
            const isHtml5Valid = authForm.checkValidity();
            const isjQueryValid = !window.$ || !$(authForm).valid || $(authForm).valid();

            if (isHtml5Valid && isjQueryValid) {
                setLoadingState(submitButton, true);
            }
        });
    }
}

// Enhanced floating label functionality
function initializeFloatingLabels() {
    const floatingInputs = document.querySelectorAll('.auth-form-floating .form-control');

    floatingInputs.forEach(input => {
        // Handle focus and blur events
        input.addEventListener('focus', function () {
            this.closest('.auth-form-floating').classList.add('focused');
        });

        input.addEventListener('blur', function () {
            if (!this.value) {
                this.closest('.auth-form-floating').classList.remove('focused');
            }
        });

        // Check if input already has value on page load
        if (input.value) {
            input.closest('.auth-form-floating').classList.add('focused');
        }
    });
}

// Loading state for buttons
function setLoadingState(button, isLoading = true) {
    if (isLoading) {
        button.disabled = true;
        button.setAttribute('data-original-text', button.innerHTML);
        button.classList.add('auth-btn-loading');
        button.innerHTML = 'Signing In...';
    } else {
        button.disabled = false;
        button.classList.remove('auth-btn-loading');
        button.innerHTML = button.getAttribute('data-original-text') || 'Sign In';
    }
}

// Auto-focus first input
function autoFocusFirstInput() {
    const firstInput = document.querySelector('.auth-form-control');
    if (firstInput) {
        setTimeout(() => {
            firstInput.focus();
        }, 100);
    }
}