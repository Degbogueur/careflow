// CareFlow Dashboard JavaScript

document.addEventListener('DOMContentLoaded', function () {
    // Initialize sidebar functionality
    initializeSidebar();

    // Initialize tooltips
    initializeTooltips();

    // Initialize smooth scrolling
    initializeSmoothScrolling();

    // Initialize fade-in animations
    initializeFadeInAnimations();

    $('.form-select2').select2({
        theme: 'bootstrap-5',
        placeholder: "-- Please select --",
        allowClear: true,
        width: '100%'
    });

    $(document).on('select2:open', function () {
        document.querySelector('.select2-container--open .select2-search__field').focus();
    });
});

// Sidebar Toggle Functionality
function initializeSidebar() {
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('main-content');
    const footer = document.querySelector('.main-footer');

    if (sidebarToggle && sidebar && mainContent) {
        sidebarToggle.addEventListener('click', function (e) {
            e.preventDefault();
            toggleSidebar();
        });
    }

    // Handle responsive behavior
    handleResponsiveSidebar();

    // Close sidebar when clicking outside on mobile
    document.addEventListener('click', function (e) {
        if (window.innerWidth <= 768) {
            const isClickInsideSidebar = sidebar && sidebar.contains(e.target);
            const isClickOnToggle = sidebarToggle && sidebarToggle.contains(e.target);

            if (!isClickInsideSidebar && !isClickOnToggle && sidebar && sidebar.classList.contains('show')) {
                closeSidebar();
            }
        }
    });

    // Handle window resize
    window.addEventListener('resize', handleResponsiveSidebar);
}

function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('main-content');
    const footer = document.querySelector('.main-footer');

    if (window.innerWidth <= 768) {
        // Mobile behavior - overlay sidebar
        if (sidebar) {
            sidebar.classList.toggle('show');
        }
    } else {
        // Desktop behavior - slide content
        if (sidebar) {
            sidebar.classList.toggle('collapsed');
        }
        if (mainContent) {
            mainContent.classList.toggle('expanded');
        }
        if (footer) {
            footer.classList.toggle('expanded');
        }
    }

    // Store sidebar state in sessionStorage
    const isCollapsed = sidebar && sidebar.classList.contains('collapsed');
    sessionStorage.setItem('sidebarCollapsed', isCollapsed.toString());
}

function closeSidebar() {
    const sidebar = document.getElementById('sidebar');
    if (sidebar) {
        sidebar.classList.remove('show');
    }
}

function handleResponsiveSidebar() {
    const sidebar = document.getElementById('sidebar');
    const mainContent = document.getElementById('main-content');
    const footer = document.querySelector('.main-footer');

    if (window.innerWidth <= 768) {
        // Mobile: Always collapsed, use overlay
        if (sidebar) {
            sidebar.classList.remove('collapsed');
            sidebar.classList.remove('show');
        }
        if (mainContent) {
            mainContent.classList.add('expanded');
        }
        if (footer) {
            footer.classList.add('expanded');
        }
    } else {
        // Desktop: Restore previous state
        const wasCollapsed = sessionStorage.getItem('sidebarCollapsed') === 'true';

        if (sidebar) {
            sidebar.classList.remove('show');
            if (wasCollapsed) {
                sidebar.classList.add('collapsed');
            } else {
                sidebar.classList.remove('collapsed');
            }
        }

        if (mainContent) {
            if (wasCollapsed) {
                mainContent.classList.add('expanded');
            } else {
                mainContent.classList.remove('expanded');
            }
        }

        if (footer) {
            if (wasCollapsed) {
                footer.classList.add('expanded');
            } else {
                footer.classList.remove('expanded');
            }
        }
    }
}

// Initialize Bootstrap tooltips
function initializeTooltips() {
    const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });
}

// Smooth scrolling for anchor links
function initializeSmoothScrolling() {
    const links = document.querySelectorAll('a[href^="#"]');

    links.forEach(link => {
        link.addEventListener('click', function (e) {
            const targetId = this.getAttribute('href').substring(1);
            const targetElement = document.getElementById(targetId);

            if (targetElement) {
                e.preventDefault();
                targetElement.scrollIntoView({
                    behavior: 'smooth',
                    block: 'start'
                });
            }
        });
    });
}

// Fade-in animations on scroll
function initializeFadeInAnimations() {
    const observerOptions = {
        threshold: 0.1,
        rootMargin: '0px 0px -50px 0px'
    };

    const observer = new IntersectionObserver(function (entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('fade-in');
                observer.unobserve(entry.target);
            }
        });
    }, observerOptions);

    // Observe elements that should fade in
    const fadeElements = document.querySelectorAll('.card, .table, .alert');
    fadeElements.forEach(el => {
        observer.observe(el);
    });
}

// Active navigation highlighting
function setActiveNavigation() {
    const currentPath = window.location.pathname;
    const navLinks = document.querySelectorAll('.sidebar .nav-link');

    navLinks.forEach(link => {
        link.classList.remove('active');

        const href = link.getAttribute('href') || '';
        if (href && (currentPath === href || currentPath.startsWith(href + '/'))) {
            link.classList.add('active');
        }
    });
}

// Call setActiveNavigation on page load
document.addEventListener('DOMContentLoaded', setActiveNavigation);

// Notification functionality
function showNotification(message, type = 'info', duration = 5000) {
    const notification = document.createElement('div');
    notification.className = `alert alert-${type} alert-dismissible fade show position-fixed`;
    notification.style.cssText = `
        top: 90px;
        right: 20px;
        z-index: 9999;
        min-width: 300px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.15);
    `;

    notification.innerHTML = `
        ${message}
        <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    `;

    document.body.appendChild(notification);

    // Auto-dismiss after duration
    setTimeout(() => {
        if (notification.parentNode) {
            notification.remove();
        }
    }, duration);
}

// Form validation helpers
function validateForm(formElement) {
    let isValid = true;
    const requiredFields = formElement.querySelectorAll('[required]');

    requiredFields.forEach(field => {
        if (!field.value.trim()) {
            field.classList.add('is-invalid');
            isValid = false;
        } else {
            field.classList.remove('is-invalid');
            field.classList.add('is-valid');
        }
    });

    return isValid;
}

// Loading state helper
function setLoadingState(element, isLoading = true) {
    if (isLoading) {
        element.disabled = true;
        element.innerHTML = '<i class="fas fa-spinner fa-spin me-2"></i>Loading...';
    } else {
        element.disabled = false;
        element.innerHTML = element.getAttribute('data-original-text') || 'Submit';
    }
}

// Search functionality
function initializeSearch() {
    const searchInput = document.getElementById('globalSearch');
    if (searchInput) {
        let searchTimeout;

        searchInput.addEventListener('input', function () {
            clearTimeout(searchTimeout);
            searchTimeout = setTimeout(() => {
                performSearch(this.value);
            }, 300);
        });
    }
}

function performSearch(query) {
    if (query.length < 2) return;

    // Implement your search logic here
    console.log('Searching for:', query);

    // Example: Filter sidebar navigation
    const navLinks = document.querySelectorAll('.sidebar .nav-link span');
    navLinks.forEach(link => {
        const text = link.textContent.toLowerCase();
        const listItem = link.closest('.nav-item');

        if (text.includes(query.toLowerCase())) {
            listItem.style.display = 'block';
        } else {
            listItem.style.display = 'none';
        }
    });
}

// Dark mode toggle (optional feature)
function toggleDarkMode() {
    document.body.classList.toggle('dark-mode');
    const isDarkMode = document.body.classList.contains('dark-mode');
    localStorage.setItem('darkMode', isDarkMode);
}

// Initialize dark mode from localStorage
function initializeDarkMode() {
    const isDarkMode = localStorage.getItem('darkMode') === 'true';
    if (isDarkMode) {
        document.body.classList.add('dark-mode');
    }
}

// Patient status helpers
function getStatusBadge(status) {
    const statusMap = {
        'healthy': 'success',
        'warning': 'warning',
        'critical': 'danger',
        'stable': 'info'
    };

    const badgeClass = statusMap[status] || 'secondary';
    return `<span class="badge bg-${badgeClass}">${status.toUpperCase()}</span>`;
}

// Export functions for use in other scripts
window.CareFlow = {
    toggleSidebar,
    showNotification,
    validateForm,
    setLoadingState,
    getStatusBadge,
    toggleDarkMode
};

// Custom Nav Tabs
// Enhanced tab functionality
function initializeHealthcareTabs() {
    const tabContainers = document.querySelectorAll('.nav-tabs-healthcare');

    tabContainers.forEach(container => {
        const tabLinks = container.querySelectorAll('.nav-link');
        const targetContainer = container.getAttribute('data-target') ||
            container.nextElementSibling;

        tabLinks.forEach(link => {
            link.addEventListener('click', function (e) {
                e.preventDefault();

                // Remove active class from all tabs in this container
                tabLinks.forEach(tab => tab.classList.remove('active'));

                // Add active class to clicked tab
                this.classList.add('active');

                // Handle tab content switching
                const targetId = this.getAttribute('data-bs-target') ||
                    this.getAttribute('href');

                if (targetId) {
                    switchTabContent(targetId.substring(1));
                }

                // Trigger custom event
                const tabEvent = new CustomEvent('healthcareTabChanged', {
                    detail: {
                        activeTab: this,
                        tabId: targetId,
                        tabText: this.textContent.trim()
                    }
                });
                document.dispatchEvent(tabEvent);
            });
        });
    });
}

function switchTabContent(tabId) {
    // Hide all tab panes
    const allTabPanes = document.querySelectorAll('.tab-pane');
    allTabPanes.forEach(pane => {
        pane.classList.remove('show', 'active');
    });

    // Show target tab pane
    const targetPane = document.getElementById(tabId);
    if (targetPane) {
        targetPane.classList.add('show', 'active');
    }
}

// Tab loading state
function setTabLoading(tabId, isLoading = true) {
    const tabPane = document.getElementById(tabId);
    if (!tabPane) return;

    if (isLoading) {
        tabPane.innerHTML = `
            <div class="tab-loading">
                <i class="fas fa-spinner fa-spin"></i>
                Loading...
            </div>
        `;
    }
}

// Update tab badge count
function updateTabBadge(tabId, count) {
    const tab = document.querySelector(`[data-bs-target="#${tabId}"], [href="#${tabId}"]`);
    if (!tab) return;

    let badge = tab.querySelector('.tab-badge');
    if (!badge && count > 0) {
        badge = document.createElement('span');
        badge.className = 'tab-badge';
        tab.appendChild(badge);
    }

    if (badge) {
        if (count > 0) {
            badge.textContent = count > 99 ? '99+' : count;
            badge.style.display = 'inline-block';
        } else {
            badge.style.display = 'none';
        }
    }
}

// Add tab dynamically
function addHealthcareTab(container, tabConfig) {
    const tabContainer = typeof container === 'string' ?
        document.querySelector(container) : container;

    if (!tabContainer) return;

    // Create tab link
    const tabItem = document.createElement('li');
    tabItem.className = 'nav-item';

    const tabLink = document.createElement('a');
    tabLink.className = 'nav-link';
    tabLink.setAttribute('data-bs-target', `#${tabConfig.id}`);
    tabLink.href = `#${tabConfig.id}`;

    let tabContent = '';
    if (tabConfig.icon) {
        tabContent += `<i class="${tabConfig.icon}"></i>`;
    }
    tabContent += tabConfig.title;
    if (tabConfig.badge) {
        tabContent += `<span class="tab-badge">${tabConfig.badge}</span>`;
    }

    tabLink.innerHTML = tabContent;
    tabItem.appendChild(tabLink);
    tabContainer.appendChild(tabItem);

    // Re-initialize event listeners
    initializeHealthcareTabs();
}

// Remove tab dynamically
function removeHealthcareTab(tabId) {
    const tabLink = document.querySelector(`[data-bs-target="#${tabId}"], [href="#${tabId}"]`);
    const tabPane = document.getElementById(tabId);

    if (tabLink) {
        tabLink.closest('.nav-item').remove();
    }

    if (tabPane) {
        tabPane.remove();
    }
}

// Initialize tabs when document is ready
document.addEventListener('DOMContentLoaded', function () {
    initializeHealthcareTabs();
});

// Export functions to global CareFlow object
if (window.CareFlow) {
    window.CareFlow.tabs = {
        initialize: initializeHealthcareTabs,
        setLoading: setTabLoading,
        updateBadge: updateTabBadge,
        addTab: addHealthcareTab,
        removeTab: removeHealthcareTab
    };
}