   M S B A M L   `   `   `  ���� H  DVisualMutator, Version=2.0.8.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:VisualMutator.Views.ConvertersVisualMutator.Views.Converters  �    �VisualMutator.Views.Resources.c__users_sysop_documents_visual_studio_2010_projects_visualmutator_visualmutator_views_resources_globalstyles_xaml\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType �� BasedOn �� Property �� Value   NormalBrush �� EndPoint �� 
StartPoint � Color � Offset  NormalBorderBrush  
GlyphBrush �� Color  MouseOverBrush  PressedBrush " newToggleButtonControlTemplate	 �� 
TargetType
 ��Name F�Name �� Triggers P� Property P� Value �� 
TargetName  newExpanderStyle � RowDefinitions ��Name ��Name �� CornerRadius � ColumnDefinitions � OverridesDefaultStyle d� 	IsChecked �� Path �� Mode �� RelativeSource   D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     ;
Converters,clr-namespace:VisualMutator.Views.Converters  %	  '��       7��'Q� �     7Q�'�� �     '�� X    7��'�� �    7��'�� �    7��'�� p    &	  �    &	 ,    &	 |    &	 �    &	 �    &	 `    &	 v    �� "  ��8   .���  � ��$ 3q���  � ��$ 	15,0,15,0q���  � ��$ 23����  � ��$ Right=���  � ��$ Center=��� "  Q�8   .���  �@Q�
IsExpanded . �� "  ��.���  � ��$ Center=���  � ��$
 Left=���  �@J�SnapsToDevicePixels . ��  � ��$
 None=���  � ��$ 0,0,2,0q��� "  ��8   .���  � ��$ Center=���  � ��$ 3,0,3,0q��� "  ��8   .���  � ��$ Center=���  � ��$ 3q���  � ��$ 20���� "  ��8   .���  � ��$ Center=���  � ��$ 3,0,3,0q���  � ��$ 25����  �@��Padding$ 3q��� "  ��.���  � ��$ 10����  � ��$ 10����  � ��$ 0,0,5,0q���  � ��
 �   ��� $	 0,1&�$	 0,0&�.��� $
 #EEE��$	 0.0Q�� $
 #CCC��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #CCC��$	 0.0Q�� $
 #444��$	 1.0Q��� $
 #444���� $	 0,1&�$	 0,0&�.��� $
 #FFF��$	 0.0Q�� $
 #AAA��$	 1.0Q��� $	 0,1&�$	 0,0&�.��� $
 #BBB��$	 0.0Q�� $
 #EEE��$	 0.1Q�� $
 #EEE��$	 0.9Q�� $
 #FFF��$	 1.0Q��� "	 d�.R�� #��z��.+��� $
 	Rectangle��$��0,0,0,0q�
������ #���  F� $ Up_Arrow��$��Center=�$��Center=�#���  "����        � 	=  	= � z     	F� $ 
Down_Arrow��$y�	Collapsed=�$��Center=�$��Center=�#���  "����     	= � 	=     � z  	= 	 P�  �@J�IsMouseOver . . ��� # �  $ 	Rectangle�� � ��P�  � �� . . ��� # �  $ 	Rectangle�� � ��P�  �@d�	IsChecked$
 trueY�. ��� $ 
Down_Arrow�� � y�$ Visible=��� $ Up_Arrow�� � y�$ 	Collapsed=��� "  9�.���  � �� �� "	 9�.R��  �� $
��Auto ��� $ 
ContentRow��$��* �.+��� $ Border��$��0��#��z��$ 2,2,0,0��._��  �� $��20 ��� $��* �.+�d� #���  #��z�� .  �� $ 
IsExpanded�$ TwoWay=� �� *TemplatedParent+�� $��1��$��4q���. $��Header���� $ 
ExpandSite��$��1��$y�	Collapsed=�$��1,0,1,1q�$ 0,0,2,2��._��� #��z��#��z��#��z��|�.   P�  �@9�
IsExpanded . . ��� $ 
ExpandSite�� � y�$ Visible=�