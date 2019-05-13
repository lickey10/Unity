﻿//----------------------------------------------
// Flip Web Apps: Game Framework
// Copyright © 2016 Flip Web Apps / Mark Hewitt
//
// Please direct any bugs/comments/suggestions to http://www.flipwebapps.com
// 
// The copyright owner grants to the end user a non-exclusive, worldwide, and perpetual license to this Asset
// to integrate only as incorporated and embedded components of electronic games and interactive media and 
// distribute such electronic game and interactive media. End user may modify Assets. End user may otherwise 
// not reproduce, distribute, sublicense, rent, lease or lend the Assets. It is emphasized that the end 
// user shall not be entitled to distribute or transfer in any way (including, without, limitation by way of 
// sublicense) the Assets in any other way than as integrated components of electronic games and interactive media. 

// The above copyright notice and this permission notice must not be removed from any files.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//----------------------------------------------

using GameFramework.Messaging.Components.AbstractClasses;
using UnityEngine;
using GameFramework.Localisation.Messages;
using GameFramework.Localisation.ObjectModel;

namespace GameFramework.Localisation.Components
{
    /// <summary>
    /// Localises an image field based upon the given Key
    /// </summary>
    [RequireComponent(typeof(UnityEngine.UI.Image))]
    [AddComponentMenu("Game Framework/Localisation/Localise Image")]
    [HelpURL("http://www.flipwebapps.com/unity-assets/game-framework/localisation/")]
    public class LocaliseImage : RunOnMessage<LocalisationChangedMessage>
    {
        /// <summary>
        /// Localization Setup.
        /// </summary>
        public LocalisableSprite Sprite;

        UnityEngine.UI.Image _imageComponent;

        /// <summary>
        /// setup
        /// </summary>
        public override void Awake()
        {
            _imageComponent = GetComponent<UnityEngine.UI.Image>();

            OnLocalise();
            base.Awake();
        }


        /// <summary>
        /// Update the display with the localise text
        /// </summary>
        void OnLocalise()
        {
            _imageComponent.sprite = Sprite.GetSprite();
        }


        /// <summary>
        /// Called whenever the localisation changes.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool RunMethod(LocalisationChangedMessage message)
        {
            OnLocalise();
            return true;
        }
    }
}