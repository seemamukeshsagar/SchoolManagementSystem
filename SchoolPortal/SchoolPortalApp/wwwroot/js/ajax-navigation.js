// ajax-navigation.js
document.addEventListener('DOMContentLoaded', function() {
    const contentArea = document.querySelector('.body-scroll');
    if (!contentArea) return;

    // Handle all navigation clicks
    document.addEventListener('click', async function(e) {
        const link = e.target.closest('a[href]');
        if (!shouldHandleLink(link)) return;
        
        e.preventDefault();
        await loadContent(link.href);
    });

    // Handle browser back/forward
    window.addEventListener('popstate', function() {
        loadContent(window.location.href, false);
    });

    async function loadContent(url, pushState = true) {
        try {
            contentArea.classList.add('loading');
            
            const response = await fetch(url, {
                headers: {
                    'X-Requested-With': 'XMLHttpRequest'
                }
            });
            
            if (!response.ok) throw new Error('Network response was not ok');
            
            const html = await response.text();
            const temp = document.createElement('div');
            temp.innerHTML = html;
            
            // If we got a full page, extract just the content
            const newContent = temp.querySelector('.body-scroll') || temp;
            contentArea.innerHTML = newContent.innerHTML;
            
            if (pushState) {
                window.history.pushState({}, '', url);
            }
            
            initScripts(newContent);
            document.dispatchEvent(new Event('content-loaded'));
            
        } catch (error) {
            console.error('Error loading page:', error);
            window.location.href = url;
        } finally {
            contentArea.classList.remove('loading');
        }
    }

    function shouldHandleLink(link) {
        if (!link) return false;
        
        // Skip if it's a special link
        if (link.target === '_blank' || 
            link.download || 
            link.getAttribute('data-ajax') === 'false' ||
            /^#|^javascript:|^mailto:|^tel:/.test(link.href) ||
            /\.(pdf|docx?|xlsx?|pptx?|zip|rar|exe|msi)$/i.test(link.href)) {
            return false;
        }
        
        // Only handle same-origin links
        return new URL(link.href).origin === window.location.origin;
    }

    function initScripts(container) {
        // Execute scripts in the new content
        const scripts = Array.from(container.querySelectorAll('script'));
        for (let script of scripts) {
            const newScript = document.createElement('script');
            if (script.src) {
                newScript.src = script.src;
                newScript.async = script.async;
            } else {
                newScript.textContent = script.textContent;
            }
            document.body.appendChild(newScript).parentNode.removeChild(newScript);
        }
    }
});