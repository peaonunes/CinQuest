﻿using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Developed by: Higor (hcmb)
/// A RewardTreeNode will have a list of rewards that will be given
/// for the player when this node is reached.
/// </summary>
[CreateAssetMenu]
[Serializable]
public class EndQuestTreeNode : DialogTreeNode
{
    [SerializeField]
    int questId;
    /// <summary>
    /// The list of the reward item ID's that the user will win after reaching this node
    /// </summary>
    public int QuestID
    {
        get { return questId; }
    }

    /// <summary>
    /// When the node is reached, gives a list of rewards for the player
    /// </summary>
    public override void Execute()
    {
		base.Execute ();
        User user = User.Instance;
        Quest quest = user.GetQuest(QuestID);
        if (quest != null && !quest.Done)
        {
            quest.Finish(user);
			AlertBox.Instance.OpenWindow (GameConstants.QUEST_COMPLETED, quest.QuestDoneMessage);
            GameManager.Instance.UpdateQuestUI();
        }
    }
}
