   M S B A M L   `   `   `  ����P  LHotelManagementSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:HotelManagementSystemHotelManagementSystem  5   	   '    !HotelManagementSystem.KitchenForm   5   	   W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:HotelManagementSystem     5   	     � Title$  KitchenForm��5   	   $	��450��6   $
��1000��6*    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius �� Triggers P� Property	 P� Value(  "System.Windows.Controls.DatePicker&   System.Windows.Controls.DataGrid=  7System.Windows.Controls.Primitives.DataGridColumnHeader
 �� Triggers �� 
TargetName��%�  '��       '�� �    '  �    '��     '�� P    '      '  A	    '�� �
    �� 5   
   " ��6   .�5      ��  � ��
 �����6+   �� 5       � ��$ 1q�60   �� 5       � ��$ 5q�6(   �� 5       � �� �#BDBDBD6,   �� 5       � �� �#3333336+   �� 5       � ��$ Segoe UI)�6+   �� 5       � ��$ 14(�6)   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� #��z��6!   #��z��5      #��z��5      $ 5��5      ._�5      �� $
��Auto=�6+   $��Disabled=�5   #   #��z��5   #   .��5   "   �� 5      5       5!      5#       5$      P� 5%       �@J�IsMouseOver$
	 True��61   . �5&      ��  � �� �#2196F364   5'      5)      �� 5+   
   " ��6   .�5,      ��  � �� �#3333336+   �� 5-       � ��$ Segoe UI)�6+   �� 5.       � ��$ 12(�6)   �� 5/       � ��$ 5q�6(   �� 50       � ��$ 1q�60   �� 51       � �� �#CCCCCC6,   �� 52       � ��$ 0,0,0,10q�6'   53      �� 55   
   "  6   .�56      ��  � �� �#3333336+   �� 57       � ��$ Segoe UI)�6+   �� 58       � ��$ 12(�6)   �� 59       � ��$ 5q�6(   �� 5:       � ��$ 1q�60   �� 5;       � �� �#CCCCCC6,   �� 5<       � ��$ 0,0,0,10q�6'   5=      �� 5?   
   " ��6   .�5@      ��  � �� �#3333336+   �� 5A       � ��$ Segoe UI)�6+   �� 5B       � ��$ 12(�6)   �� 5C       � ��$ 5q�6(   �� 5D       � ��$ 1q�60   �� 5E       � �� �#CCCCCC6,   �� 5F       � ��$ 0,0,0,10q�6'   5G      �� 5I   
   " ��6   .�5J      ��  � �� �#3333336+   �� 5K       � ��$ Segoe UI)�6+   �� 5L       � ��$ 12(�6)   �� 5M       � ��$ 0,0,10,5q�6'   5N      �� 5P   
   "  6   .�5Q      ��  � �� �#3333336+   �� 5R       � ��$ Segoe UI)�6+   �� 5S       � ��$ 12(�6)   �� 5T       � ��$ 0,10,0,0q�6'   �� 5U       � ��$ 1q�60   �� 5V       � �� �#CCCCCC6,   �� 5W       �@ RowHeaderWidth$ 0Q�6/   �� 5X       �@ CanUserResizeRows$ False��62   �� 5Y       �@ CanUserResizeColumns$ False��65   �� 5Z       �@ CanUserSortColumns$ False��63   �� 5[       �@ SelectionUnit$ FullRow=�6.   5\      �� 5^   
   "  6   .�5_      ��  � �� �#3333336+   �� 5`       � ��$ Segoe UI)�6+   �� 5a       � ��$ 12(�6)   �� 5b       � �� �#F6F6F66+   �� 5c       � ��$ 1q�60   �� 5d       � �� �#CCCCCC6,   �� 5e       � ��$ 5q�6(   �� 5f       � ��$
 Left=�6;   5g      �� 5i   
   " ��6   .�5j      ��  � �� �#2196F36+   �� 5k       � ��
 �����6+   �� 5l       � ��$
 10 5q�6(   �� 5m       � ��$ 0q�60   �� 5n       � ��$ 10q�6'   �� 5o       � ��$
 Bold"�6+   �� 5p       � �� 5q      �� 5r      " ��6&   .R�5s      � .+�5t      �� $��Border��6%   #��z��63   #��z��5u   !   #��z��5v   !   $ 5��5w   !   ._�5x   "   �� $��Center=�63   $��Center=�5y   /   5z      5{      
 5|      P� 5}       �@J�IsMouseOver$
	 True��6=   . �5~   "   �� $ Border��6)    � �� �#1976D26S   5      P� 5�       � ��$
	 True��6;   . �5�   "   �� $ Border��6)    � �� �#0D47A16S   5�      5�      5�      5�      5�      .��5�      �� -   5        .@�5�   
   �� $
��ToDo��6   .��5�      �  � ColumnDefinitions 5�      �� 5�      $��1* �6'   �� 5�      $��1* �6'   �� 5�      $��1* �6'   �� 5�      $��2* �6'   .+�5�      �� $��0��6   $��0,0,10,0q�6-   .+�5�      �� $
��Bold"�6    $��First Name:��62   �� 5�      -   5         ��Name$ FirstNameTextBlock��5�       $
��John��6<   
�������$	��5 0q�6Z   �� 5�      $
��Bold"�6    $��
Last Name:��62   �� 5�      -   5        $ LastNameTextBlock��5�       $	��Doe��6;   
�������$	��5 0q�6X   �� 5�      $
��Bold"�6    $��	Birthday:��62   �� 5�      -   5        $ BirthDayTextBlock��5�       $��1/1/2000��6;   
�������$	��5 0q�6]   �� 5�      $
��Bold"�6    $��Gender:��62   �� 5�      -   5        $ GenderTextBlock��5�       $
��Male��69   
�������$	��5 0q�6W   �� 5�      $
��Bold"�6    $��Phone Number:��62   �� 5�      -   5        $ PhoneNumberTextBlock��5�       $��01011111111��6>   
�������$	��5 0q�6c   �� 5�      $
��Bold"�6    $��Email Address:��62   �� 5�      -   5        $ EmailAddressTextBlock��5�       $��
@gmail.com��6?   
�������$	��5 0q�6c   5�      �� 5�      $��1��6   $��0,0,10,0q�6-   .+�5�      �� $
��Bold"�6    $��Number of Guests:��62   �� 5�      -   5        $ NumberOfGuestsTextBlock��5�       $��2��6A   
�������$	��5 0q�6\   �� 5�      $
��Bold"�6    $��
Room Type:��62   �� 5�      -	   5        $ RoomTypeTextBlock��5�       $��Duplex��6;   
�������$	��5 0q�6[   �� 5�      $
��Bold"�6    $��Room Floor:��62   �� 5�      -
   5        $ RoomFloorTextBlock��5�       $��First Floor��6<   
�������$	��5 0q�6a   �� 5�      $
��Bold"�6    $��Room Number:��62   �� 5�      -   5        $ RoomNumberTextBlock��5�       $	��101��6=   
�������$	��5 0q�6Z   5�      �� 5�      $��2��6   $��0,0,10,0q�6-   .+�5�      �� -   5        $��NeedsBreakfastCheckBox��5�      $��Needs Breakfast��6=   ${�False��6W   �� 5�      -   5        $��NeedsLunchCheckBox��5�      $��Needs Lunch��69   ${�False��6O   �� 5�      -   5        $��NeedsDinnerCheckBox��5�      $��Needs Dinner��6:   ${�False��6Q   �� 5�      -   5        $��NeedsCleaningCheckBox��5�      $��Needs Cleaning��6<   ${�False��6U   �� 5�      -   5        $��NeedsTowelCheckBox��5�      $��Needs Towel��69   ${�False��6O   �� 5�      -   5        $ ��HasSpecialSurpriseCheckBox��5�      $��Has Special Surprise��6A   ${�False��6`   �� 5�      -   5        $��FoodSupplyCheckBox��5�      $��Food Supply��69   5�   !   5�      �� 5�      $��3��6   $��0,0,10,0q�6-   .+�5�      �� -   5         ��Name$ ToDoListBox��5�      $��10q�63   ��5�      �� 5�      .R�5�   "   ��  �� Orientation$ 
Horizontal=�6-   .+�5�   &   �� $��0 0 5 0q�6D   ��60   �� *Id+6D   �� 5�   &   $	�� - ��60   �� 5�   &   ��60   �� *	FirstName+�� 5�   &   $�� ��60   �� 5�   &   ��60   �� *
LastName+�� 5�   &   $�� || Room Number : ��60   �� 5�   &   ��60   �� *
RoomNumber+5�   #   5�      5�      5�      5�      5�      �� 5�   
   $��Overview��6   .��5�      � .+�5�        -   5          Name$ ReservationsDataGrid��5�         AutoGenerateColumns$
 True��69      
IsReadOnly$
 True��6T      CanUserAddRows$ False��6f   5�      5�      5�      5�      