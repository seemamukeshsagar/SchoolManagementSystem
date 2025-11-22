document.addEventListener('DOMContentLoaded', function () {
	const sidebarAccordion = document.getElementById('sidebarAccordion');
	if (!sidebarAccordion) return;

	let currentOpenId = null;

	// Track which accordion panel is currently open
	sidebarAccordion.addEventListener('shown.bs.collapse', function (e) {
		currentOpenId = '#' + e.target.id;
	});

	// Prevent closing the currently open panel when its header is clicked again
	sidebarAccordion.addEventListener('click', function (e) {
		const button = e.target.closest('.accordion-button[data-bs-target]');
		if (!button) return;

		const target = button.getAttribute('data-bs-target');
		if (!target) return;

		if (currentOpenId === target) {
			e.preventDefault();
			e.stopPropagation();
		}
	});
});
