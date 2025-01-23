async function downloadDocument(documentId) {
    try {
        const response = await fetch(`https://localhost:7044/api/Documents/${documentId}/download`);

        if (!response.ok) {
            throw new Error(`Failed to download document: ${response.statusText}`);
        }

        const blob = await response.blob();

        // Extract filename from Content-Disposition header
        const contentDisposition = response.headers.get("Content-Disposition");
        let filename = "DownloadedDocument"; // Default filename

        if (contentDisposition) {
            // Match both standard and UTF-8 encoded filenames using RegExp.exec()
            const regex = /filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/;
            const match = regex.exec(contentDisposition);
            if (match?.[1]) {
                filename = match[1].replace(/['"]/g, ''); // Remove quotes
            }
        }

        // Create a Blob URL and trigger download
        const blobUrl = URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = blobUrl;
        a.download = decodeURIComponent(filename); // Decode URI components for proper filenames

        document.body.appendChild(a);
        a.click();

        document.body.removeChild(a);
        URL.revokeObjectURL(blobUrl);

        console.log(`File "${filename}" downloaded successfully!`);
    } catch (error) {
        console.error("Error downloading document:", error);
    }
}

// Attach event listener to button
document.getElementById("downloadButton").addEventListener("click", () => {
    const documentId = document.getElementById("documentId").value;

    if (!documentId) {
        alert("Please enter a valid Document ID.");
        return;
    }

    downloadDocument(documentId);
});