// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace CommunityToolkit.WinUI.Controls;

/// <summary>
/// AutomationPeer for SettingsCard
/// </summary>
public partial class SettingsCardAutomationPeer : ButtonBaseAutomationPeer
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingsCard"/> class.
    /// </summary>
    /// <param name="owner">SettingsCard</param>
    public SettingsCardAutomationPeer(SettingsCard owner)
        : base(owner)
    {
    }

    /// <summary>
    /// Gets the control type for the element that is associated with the UI Automation peer.
    /// </summary>
    /// <returns>The control type.</returns>
    protected override AutomationControlType GetAutomationControlTypeCore()
    {
        if (Owner is SettingsCard settingsCard && settingsCard.IsClickEnabled)
        {
            return AutomationControlType.Button;
        }
        else
        {
            return AutomationControlType.Group;
        }
    }

    /// <summary>
    /// Called by GetClassName that gets a human readable name that, in addition to AutomationControlType,
    /// differentiates the control represented by this AutomationPeer.
    /// </summary>
    /// <returns>The string that contains the name.</returns>
    protected override string GetClassNameCore()
    {
        return Owner.GetType().Name;
    }

    /// <inheritdoc/>
    protected override string GetNameCore()
    {
        // We only want to announce the button card name if it is clickable, else it's just a regular card that does not receive focus
        if (Owner is SettingsCard owner && owner.IsClickEnabled)
        {
            string name = AutomationProperties.GetName(owner);
            if (!string.IsNullOrEmpty(name))
            {
                return name;
            }
            else
            {
                if (owner.Header is string headerString && !string.IsNullOrEmpty(headerString))
                {
                    return headerString;
                }
            }
        }

        return base.GetNameCore();
    }

    /// <inheritdoc/>
    protected override object? GetPatternCore(PatternInterface patternInterface)
    {
        if (patternInterface == PatternInterface.Invoke)
        {
            if (Owner is SettingsCard settingsCard && settingsCard.IsClickEnabled)
            {
                // Only provide Invoke pattern if the card is clickable
                return this;
            }
            else
            {
                // Not clickable, do not provide Invoke pattern
                return null;
            }
        }

        return base.GetPatternCore(patternInterface);
    }
}
