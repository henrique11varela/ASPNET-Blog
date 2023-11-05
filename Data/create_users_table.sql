CREATE TABLE
    users (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        username TEXT,
        password TEXT,
        email TEXT,
        created_at TEXT,
        updated_at TEXT
    );

CREATE TABLE
    posts (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        title TEXT,
        body TEXT,
        accessibility INTEGER,
        created_at TEXT,
        updated_at TEXT,
        user_id INTEGER,
        FOREIGN KEY (user_id) REFERENCES users (id)
    );

CREATE TABLE
    ratings (
        id INTEGER,
        one INTEGER,
        two INTEGER,
        three INTEGER,
        four INTEGER,
        five INTEGER,
        PRIMARY KEY (id),
        FOREIGN KEY (id) REFERENCES posts (id)
    );

CREATE TABLE
    friends (
        user1_id INTEGER,
        user2_id INTEGER,
        PRIMARY KEY (user1_id, user2_id),
        FOREIGN KEY (user1_id) REFERENCES users (id),
        FOREIGN KEY (user2_id) REFERENCES users (id)
    );

CREATE TABLE
    usercookie (
        user_id INTEGER,
        cookie text,
        PRIMARY KEY (user_id),
        FOREIGN KEY (user_id) REFERENCES users (id)
    );