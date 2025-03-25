# anotation : dans Appsetting.json
 "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": "587",
    "Username": "licpfe2025@gmail.com",
    "Password": "biqppsbqyhjhsmvw" 
  } 
**ici le password est un password d'application et non pas le vrai Mdp du compte lui meme** 



# SOMMAIRE
1-**flux de connection d'objet avec la comunication avec react**
2-**flux de fonctionement des deux algorithmes de detection d'anomlie (années passé et cette année)**
3-**flux de fonctionement du systeme update position et lancement de l'urgence en utilisant OSM** 
4-**flux de fonctionement du systeme de detction de accident simulé et lancement de l'alerte en Notif**


# fLUX DE CONNECTION DES OBJET ET LANCEMENT DE LA SIMULATION (EXPLICATION AVEC LE COTER CLIENT  AUSSI )


1. **Connexion Initiale du Patient**
   - Le patient se connecte à son compte
   - Le backend récupère et renvoie ses informations personnelles
   - React stocke l'UID du patient pour une utilisation ultérieure

2. **Dashboard de Connexion des Appareils**
   - Le patient voit un ensemble de boutons pour connecter différents appareils
   - Types d'appareils : montre, téléphone, voiture, CGM
   - Règle importante : montre et voiture ne peuvent pas être connectées simultanément

3. **Processus de Connexion d'un Appareil**
   - Quand le patient clique sur "Connecter" pour un appareil :
     * React envoie une requête au contrôleur backend spécifique à ce type d'appareil
     * Contrôleur reçoit l'UID du patient
     * Le service backend génère un nouvel UID unique pour l'appareil (avec la fonction .NewGuid())
     * L'appareil est enregistré en base de données
     * La colonne "IDporteur" est remplie avec l'UID du patient
     * Si l'enregistrement réussit, le contrôleur renvoie l'UID de l'appareil à React

4. **Stockage Côté Frontend**
   - React reçoit et stocke l'UID de l'appareil
   - Met à jour l'interface pour montrer l'appareil connecté

5. **Synchronisation avec le Hub**
   - Une fois les appareils connectés en apuiant sur le bouton START
   - React construit un JSON personnalisé contenant :
     * UID du patient
     * Liste des appareils connectés (type et UID)
   - Envoi de ce JSON à un hub centralisé
   - Le hub peut traiter différents types de configurations d'appareils

# Points Clés :
- Chaque type d'appareil a son propre contrôleur backend
- Génération dynamique et unique des UIDs
- Liaison explicite entre l'appareil et le patient
- Flexibilité pour différentes combinaisons d'appareils
- Synchronisation centralisée via un hub