document.addEventListener('DOMContentLoaded', function() {
    const sidebar = document.getElementById('sidebar');
    const sidebarToggle = document.getElementById('sidebarToggle');
    const sidebarAccordion = document.getElementById('sidebarAccordion');
    
    // Initialize the sidebar state
    function initializeSidebar() {
        // Check if we have a saved state in localStorage
        const isCollapsed = localStorage.getItem('sidebarCollapsed') === 'true';
        
        // Apply the saved state
        if (isCollapsed) {
            sidebar.classList.add('collapsed');
            // Close all accordion items when initializing in collapsed state
            if (sidebarAccordion) {
                const openPanels = sidebarAccordion.querySelectorAll('.accordion-collapse.show');
                openPanels.forEach(panel => {
                    const bsCollapse = bootstrap.Collapse.getInstance(panel);
                    if (bsCollapse) {
                        bsCollapse.hide();
                    }
                });
            }
        }
        
        updateToggleIcon(isCollapsed);
    }

    // Toggle sidebar collapse
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function() {
            const isCollapsed = !sidebar.classList.contains('collapsed');
            
            // Toggle the collapsed class
            if (isCollapsed) {
                sidebar.classList.add('collapsed');
                // Close all accordion items when collapsing
                if (sidebarAccordion) {
                    const openPanels = sidebarAccordion.querySelectorAll('.accordion-collapse.show');
                    openPanels.forEach(panel => {
                        const bsCollapse = bootstrap.Collapse.getInstance(panel);
                        if (bsCollapse) {
                            bsCollapse.hide();
                        }
                    });
                }
            } else {
                sidebar.classList.remove('collapsed');
            }
            
            // Save the state to localStorage
            localStorage.setItem('sidebarCollapsed', isCollapsed);
            
            // Update the toggle icon
            updateToggleIcon(isCollapsed);
        });
    }

    // Update the toggle icon based on sidebar state
    function updateToggleIcon(isCollapsed) {
        if (!sidebarToggle) return;
        
        const icon = sidebarToggle.querySelector('i');
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