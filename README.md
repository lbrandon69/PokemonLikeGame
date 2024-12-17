
---

# **Pokemon-Like Game**

## **Description**
Ce projet est un jeu de type Pokémon développé en **C#** avec **WPF**. Il inclut une interface utilisateur ergonomique, la gestion d'une base de données SQL Server Express pour stocker les données du jeu (monstres, sorts, joueurs, etc.), et un système de combat au tour par tour.

---

## **Fonctionnalités principales**

1. **Page Setting** :  
   - Permet à l'utilisateur d'entrer une chaîne de connexion pour configurer l'accès à la base de données SQL Server. Exemple :
     ```
     Server=DESKTOP-O0ARRIL\SQLEXPRESS03;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;
     ```
   - Les informations sont validées avant d'être utilisées pour établir la connexion.

2. **Création de compte** :  
   - Les utilisateurs peuvent créer un compte avec un nom d'utilisateur et un mot de passe (le mot de passe est hashé avant d'être stocké dans la base de données).

3. **Connexion au compte** :  
   - Une fois le compte créé, l'utilisateur peut se connecter pour accéder au jeu.

4. **Gestion des monstres** :  
   - Affichage de tous les monstres disponibles dans le jeu.
   - Détails de chaque monstre : nom, points de vie (HP), image, et sorts associés.

5. **Gestion des sorts (Spells)** :  
   - Liste des sorts existants, avec affichage détaillé (nom, dégâts, description).
   - Système de tri pour voir les sorts associés à un monstre spécifique.

6. **Système de combat** :  
   - Combat au tour par tour entre un monstre joueur et un monstre ennemi généré aléatoirement.
   - Utilisation des sorts pour infliger des dégâts.
   - Barre de points de vie (HP) visible pour les deux monstres.
   - Génération automatique d'un nouvel ennemi après un combat, avec des statistiques légèrement améliorées (+10% HP ou +5% dégâts).
   - Système de score pour chaque monstre vaincu.
   - Bouton pour relancer un combat.

---

## **Base de données**

### **Script de création**
Voici le script SQL pour créer la base de données et ses tables.

```sql
-- Création de la base de données
CREATE DATABASE ExerciceMonster;
GO

-- Utilisation de la base de données
USE ExerciceMonster;
GO

-- Table Login
CREATE TABLE Login (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    PasswordHash NVARCHAR(255) NOT NULL
);

-- Table Player
CREATE TABLE Player (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    LoginID INT,
    FOREIGN KEY (LoginID) REFERENCES Login(ID)
);

-- Table Monster
CREATE TABLE Monster (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Health INT NOT NULL,
    ImageURL NVARCHAR(255) NULL
);

-- Table Spell
CREATE TABLE Spell (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL,
    Damage INT NOT NULL,
    Description NVARCHAR(MAX)
);

-- Table PlayerMonster (relation Player <-> Monster)
CREATE TABLE PlayerMonster (
    PlayerID INT NOT NULL,
    MonsterID INT NOT NULL,
    PRIMARY KEY (PlayerID, MonsterID),
    FOREIGN KEY (PlayerID) REFERENCES Player(ID),
    FOREIGN KEY (MonsterID) REFERENCES Monster(ID)
);

-- Table MonsterSpell (relation Monster <-> Spell)
CREATE TABLE MonsterSpell (
    MonsterID INT NOT NULL,
    SpellID INT NOT NULL,
    PRIMARY KEY (MonsterID, SpellID),
    FOREIGN KEY (MonsterID) REFERENCES Monster(ID),
    FOREIGN KEY (SpellID) REFERENCES Spell(ID)
);
```

---

## **Initialisation des données**

Voici un jeu de données de base que vous pouvez insérer dans la base pour démarrer le projet.

### **Insérer des utilisateurs**
```sql
INSERT INTO Login (Username, PasswordHash)
VALUES ('Player1', 'hash_player1_password'), ('Player2', 'hash_player2_password');
```

### **Insérer des joueurs**
```sql
INSERT INTO Player (Name, LoginID)
VALUES ('Ash', 1), ('Misty', 2);
```

### **Insérer des Pokémon**
```sql
INSERT INTO Monster (Name, Health, ImageURL)
VALUES 
    ('Bulbasaur', 110, 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/001.png'),
    ('Charmander', 120, 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/004.png'),
    ('Squirtle', 115, 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/007.png'),
    ('Pikachu', 100, 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/025.png'),
    ('Jigglypuff', 90, 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/039.png'),
    ('Eevee', 95, 'https://assets.pokemon.com/assets/cms2/img/pokedex/full/133.png');
```

### **Insérer des sorts**
```sql
INSERT INTO Spell (Name, Damage, Description)
VALUES 
    ('Thunder Shock', 40, 'A jolt of electricity that paralyzes the opponent.'),
    ('Flamethrower', 50, 'Unleashes a stream of fire.'),
    ('Vine Whip', 35, 'Lashes the opponent with vines.'),
    ('Water Gun', 40, 'Shoots a jet of water.'),
    ('Sing', 0, 'Puts the opponent to sleep.'),
    ('Scratch', 30, 'Scratches the opponent.');
```

### **Associer Pokémon et sorts**
```sql
INSERT INTO MonsterSpell (MonsterID, SpellID)
VALUES 
    (1, 3), (2, 2), (3, 4), (4, 1), (5, 5), (6, 6);
```

---

## **Technologies utilisées**

- **Langage** : C#
- **Framework** : WPF (Windows Presentation Foundation)
- **Base de données** : SQL Server Express
- **ORM** : Entity Framework Core

---

## **Installation et exécution**

1. Clonez le repository GitHub :
   ```bash
   git clone (https://github.com/lbrandon69/PokemonLikeGame.git)
   ```

2. Assurez-vous que SQL Server Express est installé et démarré.

3. Configurez la chaîne de connexion dans la page **Setting** lors de la première exécution :
   ```plaintext
   Server=<NomDuServeur>;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;
   ```

4. Exécutez l'application dans Visual Studio.

---

## **Structure du projet**

```
PokemonLikeProject/
│
├── Model/             # Classes pour les entités (BDD)
├── ViewModel/         # Logique métier
├── View/              # Interfaces utilisateur (XAML)
├── Data/              # Gestion de la BDD avec Entity Framework
├── Resources/         # Images et styles
└── App.xaml           # Configuration principale WPF
```

---

---

## **Auteur**
- Nom : brandon lutula

