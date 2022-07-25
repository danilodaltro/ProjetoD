Projeto criado utilizando .NET 6 e EntityFramework integrado a base SQLite com Swagger habilitado

Rotas:

- Genre

POST
*/api/Genre -> Insere novo gênero na base de dados informando nome e descrição.

PUT
*/api/Genre -> Atualiza informações de um gênero cadastrado a partir de um identificador (ID).

DELETE
*/api/Genre -> Remove um gênero cadastrado a partir de um identificador (ID).

GET
*/api/Genre -> Lista todas os gêneros cadastrados.
*/api/Genre/{id} -> Busca um gênero a partir de um identificador (ID).
*/api/Genre/name?name={name} -> Busca gêneros quais nomes tenham compatibildade com um nome informado. Ex: /api/Genre/name?name=te -> retorna gêneros que possuam te no nome, como "Terror".

- Movie

POST
*/api/Movie -> Insere novo filme na base de dados utilizando nome, id do IMDB, descrição, data de lançamento, gênero (ID), se o filme já foi assistido e nota do usuário.

PUT
*/api/Movie -> Atualiza informações de filme cadastrado a partir de um identificador (ID).

DELETE
*/api/Movie -> Remove um filme cadastrado na base a partir de um identificador (ID).

GET
*/api/Movie -> Lista todos os filmes cadastrados na base de dados incluindo informações de notas atualizadas a partir de API da OMDB.
*/api/Movie/{id} -> Retorna um filme a partir de um id informado.
*/api/Movie/name={name} -> Busca filmes quais nomes tenham compatibildade com um nome informado. Ex: /api/Movie/name?name=bl -> retorna gêneros que possuam bl no nome, como "Blade Runner".
*/api/Movie/genre?genre={genreId} -> Busca filmes a partir de um gênero informado (utilizando identificador - ID).
*/api/Movie/watched?watched={true/false} -> Busca filmes cadastrados que foram ou não assistidos.

