ConditionalBehavior
==
ConditionalBehavior to provide Condition to Behaviors for Windows Store Apps.

Install
--
ConditionalBehavior release on NuGet.
    PM> Install-Package ConditionalBehavior

Usage
--
```xml
<Page
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
    xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
    xmlns:Interactivity="using:Microsoft.Xaml.Interactivity"
    xmlns:Core="using:Microsoft.Xaml.Interactions.Core" 
    xmlns:conditionalBehavior="using:Gears.ConditionalBehavior"
    xmlns:conditions="using:Gears.ConditionalBehavior.Conditions"
    x:Class="App5.MainPage"
    mc:Ignorable="d">

	<Grid Background="{ThemeResource ApplicationPageBackgroundThemeBrush}">
		<Button Content="Click">
			<Interactivity:Interaction.Behaviors>
				<Core:EventTriggerBehavior EventName="Click">
					<conditionalBehavior:If>
                        <conditions:Equals LeftValue="{Binding Visibility,ElementName=Rectangle}"
                                           RightValue="Visible"/>
						<conditionalBehavior:If.Then>
                            <Core:ChangePropertyAction TargetObject="{Binding ElementName=Rectangle}"
                                                       PropertyName="Visibility" Value="Collapsed" />
						</conditionalBehavior:If.Then>
                        <conditionalBehavior:If.Else>
                            <Core:ChangePropertyAction TargetObject="{Binding ElementName=Rectangle}"
                                                       PropertyName="Visibility" Value="Visible" />
                        </conditionalBehavior:If.Else>
                    </conditionalBehavior:If>
				</Core:EventTriggerBehavior>
			</Interactivity:Interaction.Behaviors>
		</Button>
        <Rectangle x:Name="Rectangle" />
	</Grid>
</Page>
```

License
--
Distributed under the [MIT License][mit].

[MIT]: http://www.opensource.org/licenses/mit-license.php