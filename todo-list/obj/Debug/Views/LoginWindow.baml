   M S B A M L   `   `   `  ����D  @todo-list, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:todo_list.Helperstodo_list.Helpers  5   	   ;"clr-namespace:todo_list.ViewModelstodo_list.ViewModels  5   	   !    todo_list.Views.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   3helpersclr-namespace:todo_list.Helpers     5   	   9
viewmodels"clr-namespace:todo_list.ViewModels     5   	     � Title$  Login��5   	   $	��350��6   $	��300��6$   $	��350��5   	   $	��300��6   $	��350��5   	   $	��300��6    � WindowStartupLocation$ CenterScreen=�5	   	      PrimaryDarkBrush#��[  5
   	   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��	Username:��6   $��0��6%     MainTextStyle#��[ 62   �� 5   
   $��1��6     NormalTextBox#��[ 6^   ��6   �� *
Username+ �� UpdateSourceTrigger$ PropertyChanged=�6^   �� 5   
   $��	Password:��6   $��2��6%   #��[ 62   $��0,10,0,0q�6Y   G� 5   
   $��3��6     NormalPasswordBox#��[ 5   `   )   #todo_list.Helpers.PasswordBoxHelper   BoundPassword 6   �� *
Password+ �� Mode$ TwoWay=�6`   �� 5    
   $��4��6   
���  ��$��0,10,0,0q�6P   $
��Wrap=�6b   ��6!   �� *ErrorMessage+6b   �� 5#   
    �� Orientation$ 
Horizontal=�6   $��5��6.   $��Center=�6;   $��0,20,0,0q�6X   .+�5$      �� -   5        $��Login��5$      $	��100��6%   $
��10,0q�61     GreenButton#��[ 6?   6d   �� 5%      -   5        $��Register��5%      $	��100��6(   $
��10,0q�64     YellowButton#��[ 6B   6h   5&      5'      5(      