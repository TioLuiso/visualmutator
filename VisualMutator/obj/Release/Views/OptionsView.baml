   M S B A M L   `   `   `  ���� H  DVisualMutator, Version=2.0.8.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:VisualMutator.ViewModelsVisualMutator.ViewModels  %    VisualMutator.Views.OptionsView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     =
ViewModels&clr-namespace:VisualMutator.ViewModels     X TXceed.Wpf.Toolkit, Version=2.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$"  Options for Mutation Testing��$	��360��$	��275�� � 	Resources  �� MergedDictionaries �� Source �� 
TargetType   
checkstyle �� Property �� Value��  �� $ Resources/Global.xaml?�%b   &	        �� " ��.���  � ��$ Center=���  � ��$ 25����  � ��$ 3,0,3,0q�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��25 ��� $��25 ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� �� $
��Auto � � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+�� $��0��� $��2�� �� $
��Auto ��� �� $��5��$��0��$&�� Source files processing threads:��%  Xceed.Wpf.Toolkit.IntegerUpDown  $��5��$��1���  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]	   Maximum$	 16X�
   Minimum$
 0X��  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value �� * Options.WhiteCacheThreadsCount+ �� Mode$ TwoWay=��� $��6��$��0��$ ��Mutant processing threads:��  $��6��$��1��$	 16X�$
 1X� �� * Options.ProcessingThreadsCount+$ TwoWay=��� $��3��#��[  $!��Use Code Coverage Detection�� d� 	IsChecked �� *Options.UseCodeCoverage+�� $��4��#��[  $��Enable mutant code cache�� �� *Options.MutantsCacheEnabled+�� $��0��$��0��$)��#Max number of mutants per operator:��  $��0��$��1��$
	 1000X�$
 1X� �� *%#Options.MaxNumerOfMutantPerOperator+$ TwoWay=��� $��1��$��0��$3��-Time after which timed-out mutant is killed: ��  $��1��$��1��$		 100X�$
 1X� �� * Options.TimeFactorForMutations+$ TwoWay=��� $��2��$��0��$��Mutation order: ��  $��2��$��1��$	 2X�$
 1X� �� *Options.MutationOrder+$ TwoWay=��� $��7��$��0��$��Other Params:���� $��8��$��2������ *Options.OtherParams+$ TwoWay=�]� $��9��$��Right=�$��2��.+��� $��OK������ *CommandSave+�� $��Cancel������ *CommandClose+