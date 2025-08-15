using System;
using System.Collections.Generic;

namespace ToDo_List
{
    // Represents a single task
    class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        public TaskItem(int id, string description)
        {
            Id = id;
            Description = description;
            IsCompleted = false;
        }

        public override string ToString()
        {
            return $"{Id}. {Description} - {(IsCompleted ? "Completed" : "Pending")}";
        }
    }

    class Program
    {
        static List<TaskItem> tasks = new List<TaskItem>();
        static int nextId = 1;

        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===== To-Do List Menu =====");
                Console.WriteLine("1. Add a new task");
                Console.WriteLine("2. Mark task as completed");
                Console.WriteLine("3. View all tasks");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option (1-4): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddTask();
                        break;
                    case "2":
                        MarkTaskCompleted();
                        break;
                    case "3":
                        ViewTasks();
                        break;
                    case "4":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        static void AddTask()
        {
            Console.Clear();
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("Description cannot be empty. Press Enter to return to menu.");
                Console.ReadLine();
                return;
            }

            tasks.Add(new TaskItem(nextId++, description));
            Console.WriteLine("Task added successfully! Press Enter to return to menu.");
            Console.ReadLine();
        }

        static void MarkTaskCompleted()
        {
            Console.Clear();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks to mark as completed. Press Enter to return to menu.");
                Console.ReadLine();
                return;
            }

            ViewTasks(false); // Show tasks without waiting

            Console.Write("Enter the ID of the task to mark as completed: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                TaskItem task = tasks.Find(t => t.Id == id);
                if (task != null)
                {
                    task.IsCompleted = true;
                    Console.WriteLine("Task marked as completed! Press Enter to return to menu.");
                }
                else
                {
                    Console.WriteLine("Task ID not found. Press Enter to return to menu.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number. Press Enter to return to menu.");
            }
            Console.ReadLine();
        }

        static void ViewTasks(bool waitForUser = true)
        {
            Console.Clear();
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks available.");
            }
            else
            {
                Console.WriteLine("===== Task List =====");
                foreach (var task in tasks)
                {
                    Console.WriteLine(task);
                }
            }

            if (waitForUser)
            {
                Console.WriteLine("\nPress Enter to return to menu.");
                Console.ReadLine();
            }
        }
    }
}

