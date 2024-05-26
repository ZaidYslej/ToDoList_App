using System;
using Xamarin.Forms;
using ToDoList.Models;

namespace ToDoList
{
    public partial class TaskPage : ContentPage
    {
        public TaskPage()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var taskItem = (TaskItem)BindingContext;
            if (string.IsNullOrWhiteSpace(taskItem.Name))
            {
                await DisplayAlert("Error", "El nombre de la tarea no puede estar vacío.", "OK");
                return;
            }

            try
            {
                await App.Database.SaveTaskAsync(taskItem);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Error al guardar la tarea: " + ex.Message, "OK");
            }
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var taskItem = (TaskItem)BindingContext;
            await App.Database.DeleteTaskAsync(taskItem);
            await Navigation.PopAsync();
        }
    }
}

