Passos para executar o projeto:

1 -  Após acessar o link do github enviado por email, clicar em "Clone or downolad" -> "Download ZIP";

2 - Descompactar o projeto e abrir a solution "Empresa.Compras".
	OBS: A solution esta dividida em 3 projetos: um para a API (serviços Web Api), outro para as entidades (library) e outro WEB (Asp.net mvc).

3 - Com o projeto aberto no Visual studio, ele poderá solicitar o restore dos pacotes do Nuget packages, se solicitar, clique em Restore;

4 - No Solution Explorer, com o botão diretio em cima da solution, clique e Build Solution;

5 - Se compilou certinho, clique com o botão direito  do mouse no projeto "Empresa.Compras.Api" -> "Debug" -> "Start new instance" (isso irá subir o serviço no IIS local). Depois que carregar o browser, Copie o link localhost que aparecer na tela. Ex. "http://localhost:5677/";

6 - No Projeto "Empresa.Compras.Web" abra o arquivo web.config e cole o link copiado substituindo o original na Key "UriApi", como noexemplo:
 <add key="UriApi" value="http://localhost:5677" />;

7 - Cique com o botão diretio do mouse no projeto "Empresa.Compras.Web" -> "Debug" -> "Start new instance" isso abrirá o site para realizar as operações e consumir os serviços;

8 - Os Arquivos pdf's estão sendo salvos em "C:\Propostas" (esta funcionalidade ficou incompleta);


---Documentação API:----

O serviços estão documentados via swagger, basta colocar após o endereço da api "/swagger". Por exemplo: "http://localhost:5677/swagger"





