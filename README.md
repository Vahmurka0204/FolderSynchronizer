# FolderSynchronizer

This console application synchronizes several folders, the order of specifying folders sets the priority (from high to low).

Using the --no-delete flag, you can synchronize folders without deleting files.

The --loglevel flag sets the logging level
* summary - generates a message about the total number of deleted, updated and added files
* verbose - generates a message of the type <file_name> <action_file> from <folder_1> to <folder_2>
* silent - generates a message about the completion of synchronization
