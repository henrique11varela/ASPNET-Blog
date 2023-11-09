CREATE TABLE
    users (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        username TEXT,
        password TEXT,
        email TEXT,
        role TEXT,
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
        user_id INTEGER,
        post_id INTEGER,
        rating INTEGER,
        PRIMARY KEY (user_id, post_id),
        FOREIGN KEY (user_id) REFERENCES users (id),
        FOREIGN KEY (post_id) REFERENCES posts (id)
    );

CREATE TABLE
    comments (
        id INTEGER,
        user_id INTEGER,
        post_id INTEGER,
        body TEXT,
        created_at TEXT,
        PRIMARY KEY (id),
        FOREIGN KEY (user_id) REFERENCES users (id)
        FOREIGN KEY (post_id) REFERENCES posts (id)
    );

CREATE TABLE
    follows (
        user_id INTEGER,
        following_id INTEGER,
        PRIMARY KEY (user_id, following_id),
        FOREIGN KEY (user_id) REFERENCES users (id),
        FOREIGN KEY (following_id) REFERENCES users (id)
    );

CREATE TABLE
    usercookie (
        user_id INTEGER,
        cookie text,
        PRIMARY KEY (user_id),
        FOREIGN KEY (user_id) REFERENCES users (id)
    );

INSERT INTO 
    users (
        username, 
        email, 
        password, 
        role,
        created_at, 
        updated_at
        ) 
        VALUES (
            'admin',
            'admin@admin.admin', 
            'admin', 
            'admin', 
            '{DateTime.Now}', 
            '{DateTime.Now}');

INSERT INTO 
    usercookie (
        user_id, 
        cookie
        ) 
        VALUES (
            1,
            '{Guid.NewGuid()}'
            );