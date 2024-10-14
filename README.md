
# Pipeline CI/CD - API de Agendamento de Coleta de Lixo

Este projeto demonstra uma pipeline de CI/CD através da entrega e implantação contínuas de uma API para agendar e administrar coletas de lixo de uma cidade. A API permite criar, atualizar, cancelar e visualizar agendamentos de coleta de lixo.

A aplicação está disponível publicamente na Azure: [https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/](https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/)

- URLs relevantes:
	- Início: [https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/](https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/)
	- Lista de usuários: [https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/Users/List](https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/Users/List)
	- Lista de coletas: [https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/Coletas/List](https://test-fiap-cicd-gustavoakira-ctgafnhpbrdjckgb.brazilsouth-01.azurewebsites.net/Coletas/List)

- Endpoints relevantes - necessaŕio autenticação:
	- Coletas de lixo (GET): `/api/ColetaDeLixo`
	- Coletas por ID (GET): `/api/ColetaDeLixo/{intId}`
	- Agendar coleta (POST): `/api/ColetaDeLixo`
		- Payload: `{"dataHora": "2024-08-01T10:00:00","endereco": "Rua Abílio Soares, 537","tipoDeLixo": "Orgânico"}`
	- Deletar coleta (DELETE): `/api/ColetaDeLixo/112`

- **Nota**: O banco de dados MySQL está hospedado em uma VPS:
	- IP: 217.21.78.250
	- Hostname: gustavoakira.tech
	- Porta: 3306

## Funcionalidades da aplicação
- [x] Endpoints utilizando métodos HTTP POST, PUT, GET e DELETE
- [x] Tratamento de exceções HTTP
- [x] Utilização de autenticação para certos endpoints via chave de API
- [x] Dockerfile
- [x] Integração com SGBD MySQL
- [x] Collection do Postman para consumo da API

## Funcionamento da pipeline
1. O código é armazenado no repositório público [https://github.com/gustavoakira-sw/CICD_Pipeline](https://github.com/gustavoakira-sw/CICD_Pipeline)
2. A cada alteração no código fonte, uma Github Action é executada para construir uma imagem com base no Dockerfile. Arquivo .yml disponível em: [https://github.com/gustavoakira-sw/CICD_Pipeline/blob/master/.github/workflows/docker-image.yml](https://github.com/gustavoakira-sw/CICD_Pipeline/blob/master/.github/workflows/docker-image.yml)
```
name: Docker Image CI

on:
  push:
    branches: [ "master" ]
  pull_request:
    branches: [ "master" ]

jobs:

  build:

    runs-on: ubuntu-latest

    steps:
    - uses: actions/checkout@v4
    - name: Build the Docker image
      run: docker build . --file ./FiapApi/Dockerfile --tag my-image-name:$(date +%s)
```
3. Após o build da imagem, ela é publicada no GitHub Container Registry (GHCR) através de outra Github Action. Arquivo .yml disponível em: [https://github.com/gustavoakira-sw/CICD_Pipeline/blob/master/.github/workflows/docker-publish.yml](https://github.com/gustavoakira-sw/CICD_Pipeline/blob/master/.github/workflows/docker-publish.yml)
```
name: Docker

on:
  schedule:
    - cron: '39 9 * * *'
  push:
    branches: [ "master" ]
    tags: [ 'v*.*.*' ]
  pull_request:
    branches: [ "master" ]

env:
  REGISTRY: ghcr.io
  IMAGE_NAME: ${{ github.repository }}

jobs:
  build:
    runs-on: ubuntu-latest
    permissions:
      contents: read
      packages: write
      id-token: write

    steps:
      - name: Checkout repository
        uses: actions/checkout@v4

      - name: Install cosign
        if: github.event_name != 'pull_request'
        uses: sigstore/cosign-installer@59acb6260d9c0ba8f4a2f9d9b48431a222b68e20
        with:
          cosign-release: 'v2.2.4'

      - name: Set up Docker Buildx
        uses: docker/setup-buildx-action@f95db51fddba0c2d1ec667646a06c2ce06100226

      - name: Log into registry ${{ env.REGISTRY }}
        if: github.event_name != 'pull_request'
        uses: docker/login-action@343f7c4344506bcbf9b4de18042ae17996df046d
        with:
          registry: ${{ env.REGISTRY }}
          username: ${{ github.actor }}
          password: ${{ secrets.GITHUB_TOKEN }}

      - name: Extract Docker metadata
        id: meta
        uses: docker/metadata-action@96383f45573cb7f253c731d3b3ab81c87ef81934
        with:
          images: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}

      - name: Build and push Docker image
        id: build-and-push
        uses: docker/build-push-action@0565240e2d4ab88bba5387d719585280857ece09
        with:
          context: .             # Specify the directory where the Dockerfile is located
          file: ./FiapApi/Dockerfile       # Explicitly set the path to the Dockerfile
          push: ${{ github.event_name != 'pull_request' }}
          tags: ${{ steps.meta.outputs.tags }}
          labels: ${{ steps.meta.outputs.labels }}
          cache-from: type=gha
          cache-to: type=gha,mode=max

      - name: Sign the published Docker image
        if: ${{ github.event_name != 'pull_request' }}
        env:
          TAGS: ${{ steps.meta.outputs.tags }}
          DIGEST: ${{ steps.build-and-push.outputs.digest }}
        run: echo "${TAGS}" | xargs -I {} cosign sign --yes {}@${DIGEST}
```

4. As imagens do Docker estão disponíveis no GHCR: [https://github.com/gustavoakira-sw/CICD_Pipeline/pkgs/container/cicd_pipeline](https://github.com/gustavoakira-sw/CICD_Pipeline/pkgs/container/cicd_pipeline)
5. Foi configurada uma Aplicação Web / Web Application no portal Azure, configurada para sempre puxar a imagem mais recente do Github Container Repository, que só será disponibilizada se as actions forem executadas sem erros.

## Execução local

1. Garantir que o Docker está instalado.
2. Executar `docker pull ghcr.io/gustavoakira-sw/cicd_pipeline:master`
3. Executar `docker run <imagem>`
4. A aplicação deve executar sem problemas.
	- Nota: Não é preciso puxar o banco de dados, pois o container acessa ele pela internet.

## Evidências do funcionamento

Configuração da Azure:
![image](https://github.com/user-attachments/assets/316b666b-a608-4ce9-a019-c1f2e45bc4d2)

Configuração de deployment - Azure:
![image](https://github.com/user-attachments/assets/9d6f11f8-a55d-43cb-ab51-2a09a91339e8)

Log do Github Actions:
![image](https://github.com/user-attachments/assets/2cf83099-2e0d-4b0f-b4ad-dbefc78c5e5f)

Print da aplicação já na Azure:
![image](https://github.com/user-attachments/assets/ba8a9ee1-9345-4563-aa5a-079da0f6402c)

Validação da aplicação via Postman:
![image](https://github.com/user-attachments/assets/74f63d3c-b671-41c6-864b-c7e7e389953d)

