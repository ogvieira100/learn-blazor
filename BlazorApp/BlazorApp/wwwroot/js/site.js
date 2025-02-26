function callBlazorFunction(pageNumber) {
    DotNet.invokeMethodAsync('BlazorApp', 'OnPageSelectedJs', pageNumber);
}