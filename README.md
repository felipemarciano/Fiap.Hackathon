# Documentação – Sistema FVideos

## Introdução

Este documento tem por finalidade documentar as principais entidades, funcionalidades e tecnologias utilizadas na solução FVideos, que tem por objetivo processar vídeos tirando imagens do vídeo, compactá-las e disponibilizá-las para o usuário baixar.

## Arquitetura

A arquitetura escolhida foi utilizar o banco de dados SQL Server e hospedar as aplicações na nuvem utilizando Azure.
A solução consiste em uma aplicação front end utilizando Angular e backend com .Net 8.
Quando o usuário carregar o vídeo na aplicação, o front-end faz uma chamada de uma API para criar um registro no banco de dados com a solicitação de processamento do vídeo e salva o vídeo em um blob storage do Azure.
Em seguida, um consumer obtém as solicitações de processamento de vídeo que estão pendentes e inicia o processamento, capturando as imagens e depois compactando em um arquivo .ZIP em um blog storage do Azure.

Após o processamento ser finalizado, o usuário consegue fazer o download do .ZIP pelo front end.

## Contexto do Domínio

### Contexto Principal

- Contexto de Vídeo e Processamento de Imagens:
  - Responsável pelo gerenciamento do processo de carregamento de vídeos, processamento para extração de imagens e disponibilização das imagens processadas para download.

## Modelagem do Domínio

### Entidades

- Solicitação de processamento:
  - Representa a solicitação de processamento de imagens de um vídeo.
  - Propriedades:
    - Código único da solicitação de processamento (GUID).
    - Caminho do vídeo carregado.
    - Data da solicitação de processamento do vídeo.
    - Data de início do processamento do vídeo.
    - Data de fim do processamento do vídeo.
    - Caminho do zip com as imagens.

### Serviços do Domínio

- Serviço de Processamento de Vídeo:
  - Responsável por receber a solicitação de processamento de vídeo via API, salvar o vídeo em uma Blob Storage do Azure.
- Serviço de Compactação de Imagens:
  - Obtém todas as solicitações de processamento que estão pendentes, captura as imagens do vídeo, compacta em um arquivo .zip e salvar o arquivo no Azure Blob Storage.

## Repositórios

- Repositório de Vídeo:
  - Interface para acessar e persistir informações sobre solicitações de processamento de vídeos.
  - Implementação: SQL Server.

## Interfaces de Comunicação

- API REST:
  - Permite aos usuários carregar vídeos e baixar o arquivo .zip contendo as imagens processadas.
  - Recebe solicitações HTTP para carregar vídeos e baixar imagens.

## Fluxo de Trabalho

1. O usuário carrega um vídeo através da interface da API REST.
2. A API REST salva o vídeo em um Blob Storage e cria a solicitação de processamento do vídeo.
3. O Serviço de Processamento de Vídeo extrai imagens do vídeo, salva em um arquivo .ZIP no blob storage do Azure e salva informações sobre o processamento no SQL Server.
4. O usuário pode baixar o arquivo .zip contendo as imagens via tela.

## Tecnologias Utilizadas

- Banco de Dados: SQL Server
- Armazenamento de Arquivos: Azure Blob Storage
- Interface de Comunicação: API REST C#.NET 8.0
- Front End: Angular
- CI/CD: Azure e Kubernetes

## Banco de Dados FIAP_VIDEOS

### Estrutura do Banco de Dados

O banco de dados FIAP_VIDEOS é composto por uma única tabela: TB_SOLICITACAO_PROCESSAMENTO.

**TB_SOLICITACAO_PROCESSAMENTO**

A tabela TB_SOLICITACAO_PROCESSAMENTO armazena informações sobre as solicitações de processamento de vídeos. Ela possui os seguintes campos:

- GUID_SOLICITACAO_PROCESSAMENTO: Identificador único da solicitação de processamento de vídeo. Tipo de dado: uniqueidentifier. Chave primária.
- DC_CAMINHO_ARQUIVO: Caminho do arquivo de vídeo a ser processado. Tipo de dado: VARCHAR(2000).
- DH_SOLICITACAO: Data e hora da solicitação de processamento. Tipo de dado: DATETIME.
- DH_INICIO_PROCESSAMENTO: Data e hora de início do processamento do vídeo. Tipo de dado: DATETIME.
- DH_FIM_PROCESSAMENTO: Data e hora de término do processamento do vídeo. Tipo de dado: DATETIME.
- ID_STATUS: Status da solicitação de processamento de vídeo. Tipo de dado: TINYINT. Valores possíveis: 1 (NÃO PROCESSADO), 2 (EM PROCESSAMENTO), 3 (PROCESSADO).
- DC_CAMINHO_ZIP: Caminho do arquivo .zip gerado após o processamento do vídeo (opcional). Tipo de dado: VARCHAR(2000).

### Restrições

A tabela TB_SOLICITACAO_PROCESSAMENTO possui a seguinte restrição:

- PK_TB_SOLICITACAO_PROCESSAMENTO: Restrição de chave primária na coluna GUID_SOLICITACAO_PROCESSAMENTO.

## Conclusão

Este documento fornece uma visão geral do sistema de processamento de vídeos, incluindo sua estrutura de domínio, fluxo de trabalho e tecnologias utilizadas. Ele serve como referência para desenvolvedores, arquitetos de sistemas e outras partes interessadas no projeto.
