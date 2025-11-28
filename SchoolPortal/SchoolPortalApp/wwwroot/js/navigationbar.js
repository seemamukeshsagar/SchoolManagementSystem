document.addEventListener('DOMContentLoaded', function() {
    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebarAccordion = document.getElementById('sidebarAccordion');
    
    // Initialize the sidebar state
    function initializeSidebar() {
        const isCollapsed = localStorage.getItem('sidebarCollapsed') === 'true';
        
        if (isCollapsed) {
            sidebar.classList.add('collapsed');
        }
        
        updateToggleIcon(isCollapsed);
    }

    // Toggle sidebar collapse
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function() {
            const isCollapsed = !sidebar.classList.contains('collapsed');
            
            if (isCollapsed) {
                sidebar.classList.add('collapsed');
            } else {
                sidebar.classList.remove('collapsed');
            }
            
            localStorage.setItem('sidebarCollapsed', isCollapsed);
            updateToggleIcon(isCollapsed);
        });
    }

    // Update the toggle icon based on sidebar state
    function updateToggleIcon(isCollapsed) {
        const icon = document.getElementById('toggleIcon');
        if (!icon) return;
        
        if (isCollapsed) {
            icon.classList.remove('bi-chevron-left');
            icon.classList.add('bi-chevron-right');
        } else {
            icon.classList.remove('bi-chevron-right');
            icon.classList.add('bi-chevron-left');
        }
    }

    // Initialize the sidebar
    initializeSidebar();

    // Handle accordion behavior
    if (sidebarAccordion) {
        // When a child link is clicked, ensure its parent accordion stays open
        const navLinks = sidebarAccordion.querySelectorAll('.nav-link:not(.accordion-button)');
        navLinks.forEach(link => {
            link.addEventListener('click', function() {
                // Find the parent accordion item
                const parentItem = this.closest('.accordion-item');
                if (parentItem) {
                    const button = parentItem.querySelector('.accordion-button');
                    if (button && button.getAttribute('aria-expanded') === 'false') {
                        // If the parent is collapsed, expand it
                        const bsCollapse = new bootstrap.Collapse(button.nextElementSibling, {
                            toggle: true
                        });
                    }
                }
            });
        });

        // Close other panels when one is opened in collapsed mode
        sidebarAccordion.addEventListener('show.bs.collapse', function(e) {
            if (sidebar.classList.contains('collapsed')) {
                const allPanels = sidebarAccordion.querySelectorAll('.accordion-collapse.show');
                allPanels.forEach(panel => {
                    if (panel !== e.target) {
                        const bsCollapse = bootstrap.Collapse.getInstance(panel);
                        if (bsCollapse) {
                            bsCollapse.hide();
                        }
                    }
                });
            }
        });

        // Prevent closing the currently open panel when its header is clicked again in collapsed mode
        sidebarAccordion.addEventListener('click', function(e) {
            if (sidebar.classList.contains('collapsed')) {
                const button = e.target.closest('.accordion-button');
                if (button && button.getAttribute('aria-expanded') === 'true') {
                    e.preventDefault();
                    e.stopPropagation();
                }
            }
        });
    }
});