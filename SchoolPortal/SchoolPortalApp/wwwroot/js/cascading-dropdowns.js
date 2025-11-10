// Cascading dropdown functionality for Country, State, City
(function () {
    'use strict';

    // Initialize all cascading dropdowns on the page
    document.addEventListener('DOMContentLoaded', function () {
        initializeCascadingDropdowns();
    });

    function initializeCascadingDropdowns() {
        // Find all country dropdowns with the data attribute
        const countryDropdowns = document.querySelectorAll('select[data-states-target]');
        
        countryDropdowns.forEach(function (countryDropdown) {
            const stateTargetId = countryDropdown.getAttribute('data-states-target');
            const cityTargetId = countryDropdown.getAttribute('data-cities-target');
            const controllerName = countryDropdown.getAttribute('data-controller') || '';
            
            const stateDropdown = document.getElementById(stateTargetId);
            const cityDropdown = document.getElementById(cityTargetId);
            
            if (countryDropdown && stateDropdown) {
                // Add event listener for country change
                countryDropdown.addEventListener('change', function () {
                    const countryId = this.value;
                    clearOptions(stateDropdown, '-- Select State --');
                    clearOptions(cityDropdown, '-- Select City --');
                    
                    if (countryId) {
                        loadStates(countryId, stateDropdown, controllerName);
                    }
                });
                
                // Add event listener for state change if city dropdown exists
                if (stateDropdown && cityDropdown) {
                    stateDropdown.addEventListener('change', function () {
                        const stateId = this.value;
                        clearOptions(cityDropdown, '-- Select City --');
                        
                        if (stateId) {
                            loadCities(stateId, cityDropdown, controllerName);
                        }
                    });
                }
            }
        });
    }

    function clearOptions(selectElement, placeholder) {
        if (!selectElement) return;
        
        // Clear all options
        selectElement.innerHTML = '';
        
        // Add placeholder option
        const placeholderOption = document.createElement('option');
        placeholderOption.value = '';
        placeholderOption.textContent = placeholder;
        selectElement.appendChild(placeholderOption);
    }

    function loadStates(countryId, stateDropdown, controllerName) {
        if (!countryId || !stateDropdown) return;
        
        // Disable the dropdown while loading
        stateDropdown.disabled = true;
        
        // Determine the endpoint URL
        let url = '/GetStates';
        if (controllerName) {
            url = `/${controllerName}/GetStates`;
        }
        
        url += `?countryId=${encodeURIComponent(countryId)}`;
        
        fetch(url)
            .then(response => response.json())
            .then(data => {
                clearOptions(stateDropdown, '-- Select State --');
                
                data.forEach(item => {
                    const option = document.createElement('option');
                    option.value = item.id || item.value;
                    option.textContent = item.name || item.text;
                    stateDropdown.appendChild(option);
                });
                
                stateDropdown.disabled = false;
            })
            .catch(error => {
                console.error('Error loading states:', error);
                clearOptions(stateDropdown, '-- Select State --');
                stateDropdown.disabled = false;
            });
    }

    function loadCities(stateId, cityDropdown, controllerName) {
        if (!stateId || !cityDropdown) return;
        
        // Disable the dropdown while loading
        cityDropdown.disabled = true;
        
        // Determine the endpoint URL
        let url = '/GetCities';
        if (controllerName) {
            url = `/${controllerName}/GetCities`;
        }
        
        url += `?stateId=${encodeURIComponent(stateId)}`;
        
        fetch(url)
            .then(response => response.json())
            .then(data => {
                clearOptions(cityDropdown, '-- Select City --');
                
                data.forEach(item => {
                    const option = document.createElement('option');
                    option.value = item.id || item.value;
                    option.textContent = item.name || item.text;
                    cityDropdown.appendChild(option);
                });
                
                cityDropdown.disabled = false;
            })
            .catch(error => {
                console.error('Error loading cities:', error);
                clearOptions(cityDropdown, '-- Select City --');
                cityDropdown.disabled = false;
            });
    }

    // Expose functions globally for manual initialization if needed
    window.CascadingDropdowns = {
        initialize: initializeCascadingDropdowns,
        loadStates: loadStates,
        loadCities: loadCities
    };
})();