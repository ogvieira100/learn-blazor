function callBlazorFunction(pageNumber) {
	console.log('Page number: ' + pageNumber);
	DotNet.invokeMethodAsync('BlazorApp.Client.Componentes', 'OnPageSelectedJs', pageNumber)
		.catch(err => console.error("Erro ao chamar Blazor:", err));
		;
}