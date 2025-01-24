# Notes Project

## Installation

1. Add the `HomeController` class to your project.
2. Install the necessary NuGet packages to manage dependencies properly.
3. Configure the database connection in the `NoteContext` class.

## Features

- The `Index` method retrieves all notes from the database and passes them to a view for display to the user.
- The `Create` method handles HTTP GET requests to create a new note and displays a creation form to the user. It also passes a `SelectList` object containing all notes to the view.
- The `Create` method handles HTTP POST requests to create a new note. If the submitted data is valid, it adds the note to the database and performs the necessary redirection. If the data is invalid, it redisplays the creation form with errors.
- The `Delete` method handles HTTP POST requests to delete a specified note. It finds the note in the database, deletes it along with its child notes, and performs the necessary redirection.
- The `MoveChildNotes` method moves the child notes of a deleted note under the parent note.

## Usage

1. The `Index` method is used to display all notes on the main page of the application.
2. The `Create` method is used to create a new note. It displays the note creation form when called with an HTTP GET request and saves the new note when called with an HTTP POST request.
3. The `Delete` method is used to delete a specified note. The ID of the note to be deleted is passed as the `id` parameter.
