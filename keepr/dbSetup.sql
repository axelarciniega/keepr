CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT AUTO_INCREMENT PRIMARY KEY,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500),
        img VARCHAR(1000),
        views INT NOT NULL DEFAULT 0,
        creatorId VARCHAR(255) NOT NULL,
        kept INT NOT NULL DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(Id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

INSERT INTO
    keeps (
        name,
        description,
        img,
        creatorId
    )
VALUES (
        "dogs",
        "These are for cute dogs",
        "https://plus.unsplash.com/premium_photo-1668606717900-0ecf91e55655?auto=format&fit=crop&q=60&w=500&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8OXx8ZG9nc3xlbnwwfHwwfHx8MA%3D%3D",
        "6527182f925769ff6f109ae2"
    )

INSERT INTO
    keeps (
        name,
        description,
        img,
        creatorId
    )
VALUES (
        "Horses",
        "Lets post some horses",
        "https://images.unsplash.com/photo-1460999158988-6f0380f81f4d?auto=format&fit=crop&q=60&w=500&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Mnx8aG9yc2VzfGVufDB8fDB8fHww",
        "6527182f925769ff6f109ae2"
    )