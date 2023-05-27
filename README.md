    API .net
Bienvenue dans l'API de gestion des projets ainsi que de ses connaissances, catégories et ressources ! Cette API est construite à l'aide de la plateforme .NET et permet de réaliser des opérations CRUD (Create, Read, Update, Delete) sur différentes entités, notamment les connaissances, les catégories, les projets et les ressources.


    Installation
Pour exécuter cette API localement, suivez les étapes ci-dessous :
Assurez-vous d'avoir installé .NET Core SDK sur votre machine.
Clonez ce dépôt GitHub sur votre machine locale.
Accédez au répertoire du projet dans votre terminal ou votre invite de commandes.
Exécutez la commande suivante pour restaurer les dépendances du projet :
          dotnet restore
Ensuite, exécutez la commande suivante pour démarrer l'API :
          dotnet run
L'API sera maintenant accessible à l'adresse https://localhost:7219.


    Utilisation
Une fois l'API en cours d'exécution, vous pouvez utiliser les différentes fonctionnalités via l'interface Swagger. Ouvrez simplement https://localhost:7219/swagger/index.html

Les principales fonctionnalités fournies par cette API incluent :

Connaissances: CRUD (Create, Read, Update, Delete) pour gérer les connaissances. Vous pouvez créer de nouvelles connaissances, récupérer les connaissances existantes, les mettre à jour et les supprimer.
Catégories: CRUD pour gérer les catégories de connaissances. Vous pouvez créer de nouvelles catégories, récupérer les catégories existantes, les mettre à jour et les supprimer.
Projets: CRUD pour gérer les projets. Vous pouvez créer de nouveaux projets, récupérer les projets existants, les mettre à jour et les supprimer.
Ressources: CRUD pour gérer les ressources associées aux projets. Vous pouvez créer de nouvelles ressources, récupérer les ressources existantes, les mettre à jour et les supprimer.
Authentification
Cette API ne nécessite pas d'authentification pour l'instant. 

