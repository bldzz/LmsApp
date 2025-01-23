async function downloadDocument(documentId) {
    try {
        const response = await fetch(`https://localhost:7044/api/Documents/${documentId}/download`);

        if (!response.ok) {
            throw new Error(`Failed to download document: ${response.statusText}`);
        }

        const blob = await response.blob();

        // Extract filename from Content-Disposition header
        const contentDisposition = response.headers.get("Content-Disposition");
        const filename = getFilenameFromContentDisposition(contentDisposition);

        console.log("Content-Disposition Header:", contentDisposition);
        console.log("Extracted Filename:", filename);

        // Create a Blob URL and trigger download
        const blobUrl = URL.createObjectURL(blob);
        const a = document.createElement("a");
        a.href = blobUrl;
        a.download = filename;

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

function getFilenameFromContentDisposition(contentDisposition) {
    if (!contentDisposition) {
        console.warn("Content-Disposition header is missing.");
        return "DownloadedDocument"; // Default filename
    }

    // Regex to handle both `filename*=` (UTF-8) and `filename=`
    const regex = /filename\*=UTF-8''([^;]+)|filename="([^"]+)"|filename=([^;]+)/i;
    const match = regex.exec(contentDisposition);

    if (match) {
        // Use UTF-8 encoded filename if available, otherwise fallback to standard filename
        const utf8Filename = match[1];
        const quotedFilename = match[2];
        const unquotedFilename = match[3];

        // Decode URI-encoded UTF-8 filename or return the standard one
        return decodeURIComponent(utf8Filename || quotedFilename || unquotedFilename);
    }

    console.warn("Filename could not be extracted from Content-Disposition header.");
    return "DownloadedDocument"; // Default fallback filename
}
