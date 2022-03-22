# Capitain-CVM
Projet de démonstration d'un platformer sous Unity 2019.4

NOTE IMPORTANTE : 

La save file sera corrompue si le jeu est lancé depuis un niveau autre que le MainMenu.

S'il devait être lancé depuis une scène autre, pour reset la save file, il faut changer la valeur de la variable "debug" dans le fichier
GameManager.cs à la ligne 82 à "true", lancer le jeu depuis la scène du MainMenu, mettre fin à l'exécution du jeu,
puis remettre la valeur de la variable "debug" mentionnée précédemment à "false"
