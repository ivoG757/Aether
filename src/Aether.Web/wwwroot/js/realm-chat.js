(() => {
    const tree = document.querySelector(".realm-tree");
    const buttons = tree.querySelectorAll("[data-text-channel]");
    const title = document.getElementById("chat-title");
    const panel = document.getElementById("realm-members");
    const toggle = document.getElementById("toggle-members");
    const close = document.getElementById("close-members");

    function selectChannel(button) {

        for (const item of buttons) {

            item.removeAttribute("aria-current");
        }

        button.setAttribute("aria-current", "true");
        const name = button.querySelector(".channel-name").textContent.trim();
        title.textContent = "# " + name;
        document.getElementById("chat-status").textContent = "Message history unavailable.";
        document.getElementById("message-draft").placeholder = "Message #" + name;
    }

    for (const button of buttons) {
        button.addEventListener("click", () => selectChannel(button));
    }
    if (buttons.length) selectChannel(buttons[0]);

    function setMembers(open) {
        panel.hidden = !open;
        toggle.setAttribute("aria-expanded", String(open));
        (open ? close : toggle).focus();
    }
    toggle.addEventListener("click", () => setMembers(panel.hidden));
    close.addEventListener("click", () => setMembers(false));
    document.addEventListener("keydown", event => {
        if (event.key === "Escape" && !panel.hidden) setMembers(false);
    });
})();