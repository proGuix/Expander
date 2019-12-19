# Expander

## Contexte et besoin

Le but de ce projet est de donner des outils aux médecins afin qu'ils rédigent le plus vite et le plus clairement possible leurs ordonnances.

### Traduction des abréviations :
Les médecins utilisent de nombreuses abréviations pour désigner des pathologies, par exemple NOIA qui signifie "neuropathie optique ischémique antérieure".
Ils les utilisent afin de rédiger plus vite leurs ordonnances.
Le problème de ces abréviations est qu'elles ne sont pas clairement comprises par tout le corps médical (confrère ou pharmacien) d'où l'importance de donner la désignation exacte des abréviations.

Il faudrait permettre au médecin de créer leurs propres abréviations pour qu'un outil les utilise afin de réaliser des traductions automatiques.

### Traduction des pattern

Les médecins écrivent souvent les mêmes types de phrases et qui sont assez longues comme par exemple "1 comprimé le matin, 2 comprimés le midi, 3 comprimés le soir".
Le médecin écrairait alors "1c2c3c" et se traduirait automatiquement par l'exemple ci-dessus.

### Appliquer un style

Microsoft word est le logiciel choisi par de nombreux hopitals pour la rédaction d'ordonnance.
Une ordonnance bien stylisée est de meilleure qualité.
Une façon de gagner du temps de stylisation serait d'écrire du texte dans un langage permettant d'appliquer un style comme exemple le markdown.
Par exemple un texte écrit comme cela : ``**texte en gras**`` sera traduit par l'outil comme cela : **texte en gras**.

### Les scores

Les médecins établissent des formulaires, une fois rempli cela leur fournit un score qu'ils interprétent toujours de la meme manière afin d'établir le pronostique le plus probable.
Il faudrait intégrer ces formulaires à l'outil afin que le médecin le remplisse directement.
Le score serait alors interprété automatiquement et le pronostique serait inscrit automatiquement dans l'ordonnance.

## Objectif et périmètre
 
Ce projet consiste à créer un complément word de type .dotm.
La version minimal de word à avoir est 2010.

Le premier objectif est la mise en place d'un traducteur d'abréviation :
	* Création d'un bouton qui charge un profil. Un profil est composé d'une liste de paires d'élément : une abréviation et sa définition.
	* Création d'un bouton qui permet de choisir un profil par défaut. Ce profil par défaut sera chargé automatiquement
	* Création d'une case à cocher qui permet d'activer ou désactiver le traducteur automatiquement. Si la case est coché, dès que l'utilisateur entre une abréviation, celle-ci sera traduit automatiquement.
	* Création d'un bouton qui lorsqu'on clique effectue la traduction sur tout le document ou une sélection du document.
	
Le second objectif est la mise en place de l'application d'un style
	* Création d'un bouton qui applique le style Markdown sur tout le document ou une sélection du document