(() => {
    const tree = document.querySelector(".realm-tree");
    const workspace = document.querySelector(".realm-workspace");
    const title = document.getElementById("chat-title");
    const chatStatus = document.getElementById("chat-status");
    const draft = document.getElementById("message-draft");
    const form = document.getElementById("send-message-form");
    const sendButton = document.getElementById("send-message");
    const messageContainer = document.getElementById("messages");

    if (!tree || !workspace || !title || !chatStatus) {
        console.error("The realm chat elements were not found.");
        return;
    }

    const buttons = [...tree.querySelectorAll("[data-text-channel]")];
    const realmId = workspace.dataset.realmId;
    let activeChannelId = null;

    async function selectChannel(button) {
        activeChannelId = button.dataset.textChannel;

        await loadMessages(realmId, activeChannelId)

        for (const item of buttons) {
            item.removeAttribute("aria-current");
        }

        button.setAttribute("aria-current", "true");

        const name = button.querySelector(".channel-name").textContent.trim();
        title.textContent = "# " + name;
        chatStatus.textContent = "Message history unavailable.";

        if (draft) {
            draft.placeholder = "Message #" + name;
        }
    }

    for (const button of buttons) {
        button.addEventListener("click", () => selectChannel(button));
    }

    if (buttons.length > 0) {
        selectChannel(buttons[0]); //TODO: use default value from the service layer since I might use different approach for default channel in the future
    }

    const panel = document.getElementById("realm-members");
    const toggle = document.getElementById("toggle-members");
    const close = document.getElementById("close-members");

    if (panel && toggle && close) {
        function setMembers(open) {
            panel.hidden = !open;
            toggle.setAttribute("aria-expanded", String(open));
            (open ? close : toggle).focus();
        }

        toggle.addEventListener("click", () => setMembers(panel.hidden));
        close.addEventListener("click", () => setMembers(false));

        document.addEventListener("keydown", event => {
            if (event.key === "Escape" && !panel.hidden) {
                setMembers(false);
            }
        });
    }

    if (!form || !draft) {
        console.error("The message form was not found.");
        return;
    }

    function createMessageElement(message) {
        const article = document.createElement("article");
        article.className = "chat-message";

        const sender = document.createElement("strong");
        sender.textContent = message.senderName;

        const text = document.createElement("p");
        text.textContent = message.text ?? "";

        article.append(sender, text);
        return article;
    }

    async function sendMessage(text, realmId, channelId, token) {
        const url = `/api/realms/${encodeURIComponent(realmId)}` +
            `/channels/${encodeURIComponent(channelId)}/messages`;
        const options = {
            method: "POST",
            headers: {
                "Accept": "application/json",
                "Content-Type": "application/json",
                "RequestVerificationToken": token
            },
            body: JSON.stringify({ text: text })
        }

        const response = await fetch(url, options);

        if (!response.ok) {
            throw new Error(`Message request failed with status ${response.status}.`);
        }
    }

    async function loadMessages(realmId, channelId) {
        const url = `/api/realms/${encodeURIComponent(realmId)}` + `/channels/${encodeURIComponent(channelId)}/messages`;

        console.log("Sending GET:", url);

        try {
            const response = await fetch(url, {
                headers: { "Accept": "application/json" }
            });

            console.log("Status:", response.status);
            console.log("Content type:", response.headers.get("content-type"));

            const body = await response.text();
            console.log("Response body:", body);

            if (!response.ok) {
                throw new Error(`GET failed with status ${response.status}`);
            }

            const messages = JSON.parse(body);
            console.log("Messages:", messages);

            for (const message of messages.slice().reverse()) {
                messageContainer.append(createMessageElement(message));
            }


        } catch (error) {
            console.error("Could not load messages:", error);
        }
    }

    form.addEventListener("submit", async event => {
        event.preventDefault();

        const text = draft.value.trim();
        const tokenInput = form.querySelector('[name="__RequestVerificationToken"]');

        if (!text) {
            return;
        }

        if (!realmId || !activeChannelId) {
            chatStatus.textContent = "Select a text channel before sending a message.";
            return;
        }

        if (!tokenInput) {
            chatStatus.textContent = "The security token is missing. Reload the page and try again.";
            console.error("The antiforgery token was not found in the message form.");
            return;
        }

        if (sendButton) {
            sendButton.disabled = true;
        }

        try {
            await sendMessage(text, realmId, activeChannelId, tokenInput.value);

            draft.value = "";

            chatStatus.textContent = "Message sent. Message history is not displayed yet.";
        } catch (error) {

            console.error(error);

            chatStatus.textContent = "Could not send the message. Your draft is still here.";

        } finally {

            if (sendButton) {
                sendButton.disabled = false;
            }

        }
    });
})();
