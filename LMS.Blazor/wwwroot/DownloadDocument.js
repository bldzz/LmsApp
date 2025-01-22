async function downloadDocument(documentId) {
    try {
        // Fetch the file from the API
        const response = await fetch(`https://localhost:7044/api/Documents/${documentId}/download`);

        if (!response?.ok) { // Use optional chaining for concise null/undefined checks
            throw new Error(`Failed to download document: ${response?.statusText || "Unknown error"}`);
        }

        // Convert response to a Blob
        const blob = await response.blob();

        // Create a temporary URL for the Blob
        const blobUrl = URL.createObjectURL(blob);

        // Create an anchor element to trigger the download
        const a = document.createElement("a");
        a.href = blobUrl;

        // Set the filename for the downloaded file
        const contentDisposition = response.headers.get("Content-Disposition");
        let filename = "DownloadedDocument"; // Default filename

        if (contentDisposition?.includes("filename=")) { // Use optional chaining for safety
            const match = /filename="?(.+?)"?$/.exec(contentDisposition); // Use RegExp.exec() for better handling
            if (match) {
                filename = match[1];
            }
        }

        a.download = filename;

        // Append the anchor to the body, trigger click, and remove it
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        // Revoke the Blob URL after use
        URL.revokeObjectURL(blobUrl);

        console.log("File downloaded successfully!");
    } catch (error) {
        console.error("Error downloading document:", error);
    }
}

document.getElementById("downloadButton").addEventListener("click", () => {
    // Get the value from the input field
    const documentId = document.getElementById("documentId").value;

    // Check if the input is valid
    if (!documentId) {
        alert("Please enter a valid Document ID.");
        return;
    }

    // Call the downloadDocument function with the entered ID
    downloadDocument(documentId);
});
