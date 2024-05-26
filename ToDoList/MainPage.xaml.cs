using Xamarin.Forms;
using ToDoList.Models;
using System;

namespace ToDoList
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            tasksListView.ItemsSource = await App.Database.GetTasksAsync();
        }

        async void OnAddTaskClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TaskPage
            {
                BindingContext = new TaskItem()
            });
        }

        async void OnTaskSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new TaskPage
                {
                    BindingContext = e.SelectedItem as TaskItem
                });
            }
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
    }
}

